using LibraryC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryC.DTOs
{
    public class LivroBibliotecaResponseDTO
    {
        [Required]
        public int IdLivroBiblioteca { get; set; }

        public int IdLivro { get; set; }

        public int IdBiblioteca { get; set; }

        public int Quantidade { get; set; }
    }
}
