using Biblioteca.Contracts.Service;
using Biblioteca.Dto.Usuario;
using Biblioteca.Mapper;
using Biblioteca.Models;
using Biblioteca.Validations.Notification;
using MongoDB.Driver;
using Biblioteca.Dto.token;

namespace Biblioteca.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMongoDatabase _database;
        private readonly TokenService _tokenService;

        public UsuarioService(IMongoDatabase database, TokenService tokenService)
        {
            _database = database;
            _tokenService = tokenService;
        }

        public async Task<Notifiable> CadastraUsuario(IncluirUsuarioRequest incluirUsuarioRequest)
        {
            var response = new TokenResponse();

            var collection = _database.GetCollection<Usuario>("usuario");

            Usuario usuario = MapperUsuario.UsuarioMapperToDto(incluirUsuarioRequest);

            var senhaHash = Usuario.Hash(usuario.Senha);

            usuario.SetSenha(senhaHash);

            await collection.InsertOneAsync(usuario);

            return response;
        }

        public async Task<TokenResponse> Login(LoginRequest loginRequest)
        {
            var response = new TokenResponse();

            var collection = _database.GetCollection<Usuario>("usuario");

            var filtro = Builders<Usuario>.Filter.Eq(x => x.Email, loginRequest.Email);

            var usuario = await collection.Find(filtro).FirstOrDefaultAsync();


            if (usuario == null)
            {
                response.AddNotification(new Notification("Email ou senha incorreta!"));
                return response;
            }

            if (usuario.SenhaHash != Usuario.Hash(loginRequest.Senha))
            {
                response.AddNotification(new Notification("Email ou senha incorreta!"));
                return response;
            }

            response.Token = _tokenService.GenerateToken(usuario);
            return response;
        }
    }
}
