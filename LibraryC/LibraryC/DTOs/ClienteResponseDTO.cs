using System.ComponentModel.DataAnnotations;

namespace LibraryC.DTOs
{
    public class ClienteResponseDTO
    {
        [Required]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }

        [StringLength(14)]
        public string Cpf { get; set; }
    }
}
