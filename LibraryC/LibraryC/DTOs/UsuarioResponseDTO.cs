using System.ComponentModel.DataAnnotations;

namespace LibraryC.DTOs
{
    public class UsuarioResponseDTO
    {
        [Required]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Senha { get; set; }

        [Required]
        [StringLength(20)]
        public string Perfil { get; set; }

        [StringLength(14)]
        public string Cpf { get; set; }
    }
}
