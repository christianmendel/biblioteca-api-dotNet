using Biblioteca.Validations.Notification;

namespace Biblioteca.Dto.token
{
    public class TokenResponse: Notifiable
    {
        public string Token { get; set; }
    }
}
