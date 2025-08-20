using Biblioteca.Contracts.Service;
using Biblioteca.Dto.Livro;
using Biblioteca.Dto.Usuario;
using Biblioteca.Mapper;
using Biblioteca.Models;
using Biblioteca.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BibliotecaController : Controller
    {

        private readonly IBibliotecaService _bibliotecaService;

        public BibliotecaController(IBibliotecaService bibliotecaService)
        {
            _bibliotecaService = bibliotecaService;

        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> IncluirLivro([FromBody] IncluirLivroRequest incluirLivroRequest)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type.Equals("_id"))?.Value;

            var response = await _bibliotecaService.IncluirLivro(incluirLivroRequest, userId);

            if (!response.IsValid())
                return NotFound(response.Notifications);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<LivroResponse>>> ListarLivro()
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type.Equals("_id"))?.Value;

            var response = await _bibliotecaService.ListarLivro(userId);

            if (!response.All(livro => livro.IsValid()))
            {
                var notValidBooks = response.Where(livro => !livro.IsValid()).Select(livro => livro.Notifications);
                return BadRequest(notValidBooks);
            }

            return Ok(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<LivroResponse>> ObterLivro([FromRoute] string id)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type.Equals("_id"))?.Value;

            var response = await _bibliotecaService.ObterLivro(id, userId);

            if (!response.IsValid())
                return NotFound(response.Notifications);

            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<LivroResponse>> AlterarLivro([FromRoute] string id, [FromBody] AlterarLivroRequest alterarLivroRequest)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type.Equals("_id"))?.Value;

            var response = await _bibliotecaService.AlterarLivro(id, alterarLivroRequest, userId);

            if (!response.IsValid())
                return NotFound(response.Notifications);

            return Ok(response);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarLivro([FromRoute] string id)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type.Equals("_id"))?.Value;

            var response = await _bibliotecaService.DeletarLivro(id, userId);

            if (!response.IsValid())
                return NotFound(response.Notifications);

            return Ok();
        }
    }
}
