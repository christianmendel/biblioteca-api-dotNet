using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Dto.Livro
{
    public class IncluirLivroRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int Tipo { get; set; }

    }
}
