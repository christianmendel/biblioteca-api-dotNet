using Biblioteca.Dto.Livro;
using Biblioteca.Settings.Validations;

namespace Biblioteca.Contracts.Service
{
    public interface IBibliotecaService
    {
        Task<Notifiable> IncluirLivro(IncluirLivroRequest incluirLivroRequest, string userId);
        Task<List<LivroResponse>> ListarLivro(string userId);
        Task<LivroResponse> ObterLivro(string id, string userId);
        Task<LivroResponse> AlterarLivro(string id, AlterarLivroRequest alterarLivroRequest, string userId);
        Task<LivroResponse> DeletarLivro(string id, string userId);
    }
}
