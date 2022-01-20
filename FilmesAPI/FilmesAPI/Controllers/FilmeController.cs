using AutoMapper;
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
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost] // Verbo POST = inserir (create do CRUD)
        public IActionResult AdicionaFilme([FromBody] CreateFilmeDTO filmeDTO) // O [FromBody] indica que a requisição será passada via corpo da mensagem (JSON)
        {
            // Convertendo através do mapeamento
            Filme filme = _mapper.Map<Filme>(filmeDTO);

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
                // Convertendo através do mapeamento
                ReadFilmeDTO filmeDTO = _mapper.Map<ReadFilmeDTO>(filme);

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

            // Transformando nosso filmeDTONovo em Filme através do mapeamento
            _mapper.Map(filmeDTONovo, filme);

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
