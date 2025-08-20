using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Dto.Livro
{
    public class AlterarLivroRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
    }
}
