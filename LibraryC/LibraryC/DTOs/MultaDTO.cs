using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LibraryC.DTOs
{
    public class MultaDTO
    {
        public int IdCliente { get; set; }

        public decimal Valor { get; set; }

        [Required]
        [StringLength(255)]
        public string Motivo { get; set; }
    }
}
