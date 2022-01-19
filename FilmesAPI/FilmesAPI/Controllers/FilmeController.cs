using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]            // Annotation para especificar que essa classe é uma API Controller
    [Route("[controller]")]    // A rota acessada será o nome do que vem antes do Controller
    public class FilmeController : ControllerBase
    {
        private static List<Filme> filmes = new List<Filme>();

        [HttpPost] // Verbo POST = inserir (create do CRUD)
        public void AdicionaFilme([FromBody] Filme filme) // O [FromBody] indica que a requisição será passada via corpo da mensagem (JSON)
        {
            filmes.Add(filme);
             
        }
    }
}
