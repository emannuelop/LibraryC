using LibraryC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryC.DTOs
{
    public class MultaResponseDTO
    {
        [Required]
        public int IdMulta { get; set; }

        public int IdCliente { get; set; }

        public decimal Valor { get; set; }

        public DateOnly Data { get; set; }

        public string Motivo { get; set; }

        public string Status { get; set; }
    }
}
