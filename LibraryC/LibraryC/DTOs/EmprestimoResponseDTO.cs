using LibraryC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace LibraryC.DTOs
{
    public class EmprestimoResponseDTO
    {
        [Required]
        public int IdEmprestimo { get; set; }

        public int? IdCliente { get; set; }

        public int? IdLivro { get; set; }

        public DateOnly DataEmprestimo { get; set; }

        public DateOnly? DataDevolucao { get; set; }

        public DateOnly DataPrevistaDevolucao { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
