using System.ComponentModel.DataAnnotations;

namespace LibraryC.DTOs
{
    public class LivroDTO
    {

        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }

        public int? AnoPublicacao { get; set; }

        public int? IdAutor { get; set; }
    }
}
