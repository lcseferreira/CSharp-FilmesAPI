using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmeContext : DbContext // Contexto de conexão entre a aplicação e o banco de dados
    {
        public FilmeContext(DbContextOptions<FilmeContext> opt) : base(opt)
        {

        }

        // Contexto para usarmos no controller
        public DbSet<Filme> Filmes { get; set; }
    }
}
