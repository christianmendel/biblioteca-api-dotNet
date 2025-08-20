using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Dto.Usuario
{
    public class LoginRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
