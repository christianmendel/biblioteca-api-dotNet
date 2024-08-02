using Biblioteca.Contracts.Service;
using Biblioteca.Dto.Livro;
using Biblioteca.Mapper;
using Biblioteca.Models;
using Biblioteca.Settings.Validations;
using MongoDB.Driver;

namespace Biblioteca.Services
{
    public class BibliotecaService: IBibliotecaService
    {
        private readonly IMongoDatabase _database;
        private readonly TokenService _tokenService;

        public BibliotecaService(IMongoDatabase database, TokenService tokenService)
        {
            _database = database;
            _tokenService = tokenService;
        }

        public async Task<Notifiable> IncluirLivro(IncluirLivroRequest incluirLivroRequest, string userId)
        {
            var response = new Notifiable();

            var collection = _database.GetCollection<Livro>("livro");

            Livro novoLivro = MapperLivro.LivroMapperToDto(incluirLivroRequest);

            novoLivro.SetUser(userId);

            await collection.InsertOneAsync(novoLivro);

            return response;
        }

        public async Task<List<LivroResponse>> ListarLivro(string userId)
        {
            var response = new List<LivroResponse>();

            var collection = _database.GetCollection<Livro>("livro");

            var filtro = Builders<Livro>.Filter.Eq(x => x.UserId, userId);

            var livros = await collection.Find(filtro).ToListAsync();

            List<LivroResponse> livrosResponse = new List<LivroResponse>();

            foreach (var item in livros)
            {
                livrosResponse.Add(MapperLivro.LivroMapperViewDto(item));
            }

            response = livrosResponse;

            return response;
        }

        public async Task<LivroResponse> ObterLivro(string id, string userId)
        {
            var response = new LivroResponse();

            var collection = _database.GetCollection<Livro>("livro");

            var filtro = Builders<Livro>.Filter.And(
                Builders<Livro>.Filter.Eq(x => x._id, id),
                Builders<Livro>.Filter.Eq(x => x.UserId, userId)
            );

            var livro = await collection.Find(filtro).FirstOrDefaultAsync();

            if (livro == null)
            {
                response.AddNotification(new Notification("Livro não encotrado!"));
                return response;
            }

            var livroResponse = MapperLivro.LivroMapperViewDto(livro);

            response = livroResponse;

            return response;
        }

        public async Task<LivroResponse> AlterarLivro(string id, AlterarLivroRequest alterarLivroRequest, string userId)
        {
            var response = new LivroResponse();

            var collection = _database.GetCollection<Livro>("livro");

            var filtro = Builders<Livro>.Filter.And(
                Builders<Livro>.Filter.Eq(x => x._id, id),
                Builders<Livro>.Filter.Eq(x => x.UserId, userId)
            );

            var livro = await collection.Find(filtro).FirstOrDefaultAsync();

            if (livro == null)
            {
                response.AddNotification(new Notification("Livro não encotrado!"));
                return response;
            }

            var updateDef = Builders<Livro>.Update
                .Set(x => x.Nome, alterarLivroRequest.Nome)
                .Set(x => x.Descricao, alterarLivroRequest.Descricao);

            await collection.UpdateOneAsync(filtro, updateDef);

            var livroResponse = MapperLivro.LivroMapperViewDto(livro);

            response = livroResponse;

            return response;
        }

        public async Task<LivroResponse> DeletarLivro(string id, string userId)
        {
            var response = new LivroResponse();

            var collection = _database.GetCollection<Livro>("livro");

            var filtro = Builders<Livro>.Filter.And(
                Builders<Livro>.Filter.Eq(x => x._id, id),
                Builders<Livro>.Filter.Eq(x => x.UserId, userId)
            );

            var livro = await collection.Find(filtro).FirstOrDefaultAsync();

            if (livro == null)
            {
                response.AddNotification(new Notification("Livro não encotrado!"));
                return response;
            }

            await collection.DeleteOneAsync(filtro);

            return response;
        }
    }
}
