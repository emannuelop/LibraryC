using System.ComponentModel.DataAnnotations;

namespace LibraryC.DTOs
{
    public class AutorDTO
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
    }
}
