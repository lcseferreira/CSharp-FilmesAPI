using FilmesAPI.Data;
using FilmesAPI.Data.DTOs;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController] // Annotation para especificar que essa classe é uma API Controller
    [Route("[controller]")] // A rota acessada será o nome do que vem antes do Controller
    public class FilmeController : ControllerBase
    {

        // Fazendo a injeção de dependência
        private FilmeContext _context;

        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        [HttpPost] // Verbo POST = inserir (create do CRUD)
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDTO filmeDTO) // O [FromBody] indica que a requisição será passada via corpo da mensagem (JSON)
        {
            // Criando um objeto com construtor implícito
            Filme filme = new Filme // Convertendo CreateFilmeDTO em Filme
            {
                Titulo = filmeDTO.Titulo,
                Diretor = filmeDTO.Diretor,
                Genero = filmeDTO.Genero,
                Duracao = filmeDTO.Duracao
            };

            _context.Filmes.Add(filme); // Contexto usado para adicionarmos o filme no banco de dados
            _context.SaveChanges(); // Salvando as alterações no banco
            return CreatedAtAction(nameof(RecuperaFilmesPorID), new { Id = filme }, filme); // Retorna o status (201) e onde ele está sendo criado (Location no HEAD)
        }

        [HttpGet] // Verbo GET = obter (read do CRUD)
        public IActionResult RecuperaFilmes()
        {
            return Ok(_context.Filmes);
        }

        [HttpGet("{id}")] // Obtendo dados passando um parâmetro
        public IActionResult RecuperaFilmesPorID(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id); // Retorna o primeiro resultado ou null

            if (filme != null)
            {
                ReadFilmeDTO filmeDTO = new ReadFilmeDTO
                {
                    Id = filme.Id,
                    Titulo = filme.Titulo,
                    Diretor = filme.Diretor,
                    Genero = filme.Genero,
                    Duracao = filme.Duracao,
                    HoraDaConsulta = DateTime.Now
                };

                return Ok(filmeDTO); // Retornando OK se acharmos o filme
            }
            return NotFound(); // Retornando NotFound se não acharmos o filme
        }

        [HttpPut("{id}")] // Verbo PUT = update do CRUD
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDTO filmeDTONovo)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            filme.Titulo = filmeDTONovo.Titulo;
            filme.Diretor = filmeDTONovo.Diretor;
            filme.Genero = filmeDTONovo.Genero;
            filme.Duracao = filmeDTONovo.Duracao;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")] // Verbo DELETE = Delete do CRUD
        public IActionResult RemoveFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            _context.Remove(filme);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
