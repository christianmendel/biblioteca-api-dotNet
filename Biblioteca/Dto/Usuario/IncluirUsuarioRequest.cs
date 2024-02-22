using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Dto.Usuario
{
    public class IncluirUsuarioRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
