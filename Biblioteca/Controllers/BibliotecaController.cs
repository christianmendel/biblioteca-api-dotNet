using Biblioteca.Dto.Livro;
using Biblioteca.Mapper;
using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Biblioteca.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BibliotecaController : Controller
    {

        private readonly IMongoDatabase _database;

        public BibliotecaController(IMongoDatabase database)
        {
            _database = database;

        }


        [HttpPost]
        public async Task<ActionResult> IncluirLivro([FromBody] IncluirLivroRequest incluirLivroRequest)
        {
            var collection = _database.GetCollection<Livro>("livro");

            Livro novoLivro = MapperLivro.LivroMapperToDto(incluirLivroRequest);

            await collection.InsertOneAsync(novoLivro);

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<LivroResponse>>> ListarLivro()
        {
            var collection = _database.GetCollection<Livro>("livro");
            var livros = await collection.Find(new BsonDocument()).ToListAsync();

            List<LivroResponse> livrosResponse = new List<LivroResponse>();

            foreach (var item in livros)
            {
                livrosResponse.Add(MapperLivro.LivroMapperViewDto(item));
            }

            return livrosResponse;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivroResponse>> ObterLivro([FromRoute] string id)
        {
            var collection = _database.GetCollection<Livro>("livro");

            var filtro = Builders<Livro>.Filter.Eq(x => x._id, id);

            var livro = await collection.Find(filtro).FirstOrDefaultAsync();

            if (livro == null) return NotFound();

            var livroResponse = MapperLivro.LivroMapperViewDto(livro);

            return livroResponse;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LivroResponse>> AlterarLivro([FromRoute] string id, [FromBody] AlterarLivroRequest alterarLivroRequest)
        {
            var collection = _database.GetCollection<Livro>("livro");

            var filtro = Builders<Livro>.Filter.Eq(x => x._id, id);

            var livro = await collection.Find(filtro).FirstOrDefaultAsync();

            if (livro == null) return NotFound();

            var updateDef = Builders<Livro>.Update
                .Set(x => x.Nome, alterarLivroRequest.Nome)
                .Set(x => x.Descricao, alterarLivroRequest.Descricao);

            await collection.UpdateOneAsync(filtro, updateDef);

            var livroResponse = MapperLivro.LivroMapperViewDto(livro);

            return livroResponse;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletarLivro([FromRoute] string id)
        {
            var collection = _database.GetCollection<Livro>("livro");

            var filtro = Builders<Livro>.Filter.Eq(x => x._id, id);

            var livro = await collection.Find(filtro).FirstOrDefaultAsync();

            if (livro == null) return NotFound();

            await collection.DeleteOneAsync(filtro);


            return Ok();
        }
    }
}
