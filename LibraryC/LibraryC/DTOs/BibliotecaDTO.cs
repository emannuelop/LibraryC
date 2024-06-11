using System.ComponentModel.DataAnnotations;

namespace LibraryC.DTOs
{
    public class BibliotecaDTO
    {

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255)]
        public string Endereco { get; set; }
    }
}
