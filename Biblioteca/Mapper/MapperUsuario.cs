using Biblioteca.Dto.Usuario;
using Biblioteca.Models;

namespace Biblioteca.Mapper
{
    public class MapperUsuario
    {
        public static Usuario UsuarioMapperToDto(IncluirUsuarioRequest incluirUsuarioRequest)
        {
            return new Usuario(incluirUsuarioRequest.Nome, incluirUsuarioRequest.Email, incluirUsuarioRequest.Senha);
        }

    }
}
