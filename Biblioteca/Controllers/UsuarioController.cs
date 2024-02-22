using Biblioteca.Dto.Usuario;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Biblioteca.Settings;
using Amazon.Runtime.Internal;
using Biblioteca.Contracts.Service;
using Biblioteca.Dto.token;
using Biblioteca.Validations.Notification;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("cadastro")]
        public async Task<ActionResult<Notifiable>> CadastraUsuario([FromBody] IncluirUsuarioRequest incluirUsuarioRequest)
        {
            var response = await _usuarioService.CadastraUsuario(incluirUsuarioRequest);

            if (!response.IsValid())
                return NotFound(response.Notifications);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await _usuarioService.Login(loginRequest);

            if (!response.IsValid())
                return NotFound(response.Notifications);

            return Ok(response);
        }
    }
}
