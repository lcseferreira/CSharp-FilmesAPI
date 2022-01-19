using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController] // Annotation para especificar que essa classe é uma API Controller
    [Route("[controller]")] // A rota acessada será o nome do que vem antes do Controller
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();

        [HttpPost] // Verbo POST = inserir (create do CRUD)
        public IActionResult AdicionaFilme([FromBody] Filme filme) // O [FromBody] indica que a requisição será passada via corpo da mensagem (JSON)
        {
            filmes.Add(filme);

            return CreatedAtAction(nameof(RecuperaFilmesPorID), new { Id = filme }, filme); // Retorna o status (201) e onde ele está sendo criado (Location no HEAD)
        }

        [HttpGet] // Verbo GET = obter (read do CRUD)
        public IActionResult RecuperaFilmes()
        {
            return Ok(filmes);
        }

        [HttpGet("{id}")] // Obtendo dados passando um parâmetro
        public IActionResult RecuperaFilmesPorID(int id)
        {
            Filme filme = filmes.FirstOrDefault(filme => filme.Id == id); // Retorna o primeiro resultado ou null

            if (filme != null)
            {
                return Ok(filme); // Retornando OK se acharmos o filme
            }
            return NotFound(); // Retornando NotFound se não acharmos o filme
        }
    }
}
