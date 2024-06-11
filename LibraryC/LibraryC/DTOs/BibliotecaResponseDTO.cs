using LibraryC.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryC.DTOs
{
    public class BibliotecaResponseDTO
    {
        [Required]
        public int IdBiblioteca { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(255)]
        public string Endereco { get; set; }
    }
}
