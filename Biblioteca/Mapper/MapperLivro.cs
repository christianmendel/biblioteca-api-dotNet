using Biblioteca.Dto.Livro;
using Biblioteca.Models;

namespace Biblioteca.Mapper
{
    public class MapperLivro
    {
        public static Livro LivroMapperToDto(IncluirLivroRequest incluirLivroRequest)
        {
            return new Livro(incluirLivroRequest.Nome, incluirLivroRequest.Descricao, (TipoLivro)Enum.ToObject(typeof(TipoLivro), incluirLivroRequest.Tipo));
        }

        public static LivroResponse LivroMapperViewDto(Livro livro)
        {
            var retorno = new LivroResponse();

            retorno._id = livro._id;
            retorno.Nome = livro.Nome;
            retorno.Descricao = livro.Descricao;
            retorno.Tipo =  retorno.Tipo;

            return retorno;
        }
    }
}
