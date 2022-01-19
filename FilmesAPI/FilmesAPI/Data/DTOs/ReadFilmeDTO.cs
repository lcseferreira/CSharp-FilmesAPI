using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FilmesAPI.Data.DTOs
{
    public class ReadFilmeDTO
    {
        [Key] // Annotation de Primary Key
        [Required]
        [Column(name: "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O título do filme é obrigatório.")]
        [Column(name: "TITULO")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O diretor do filme é obrigatório.")]
        [Column(name: "DIRETOR")]
        public string Diretor { get; set; }

        [Column(name: "GENERO")]
        public string Genero { get; set; }

        [Range(1, 600, ErrorMessage = "O filme deve ter no mínimo 1 minuto e no máximo 600 minutos.")]
        [Column(name: "DURACAO")]
        public int Duracao { get; set; }
        
        public DateTime HoraDaConsulta { get; set; }
    }
}
