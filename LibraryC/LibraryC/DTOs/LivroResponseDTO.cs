using LibraryC.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryC.DTOs
{
    public class LivroResponseDTO
    {
        [Required]
        public int IdLivro { get; set; }

        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }

        public int? AnoPublicacao { get; set; }

        public int? IdAutor { get; set; }
    }
}
