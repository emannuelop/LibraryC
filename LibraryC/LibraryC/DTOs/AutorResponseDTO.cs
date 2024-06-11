using LibraryC.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryC.DTOs
{
    public class AutorResponseDTO
    {

        [Required]
        public int IdAutor { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
    }
}
