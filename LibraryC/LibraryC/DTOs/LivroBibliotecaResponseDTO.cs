using LibraryC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryC.DTOs
{
    public class LivroBibliotecaResponseDTO
    {
        [Required]
        public int IdLivroBiblioteca { get; set; }

        public int? Livro { get; set; }

        public int? Biblioteca { get; set; }

        public int Quantidade { get; set; }
    }
}
