

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Api.DTOS
{
    public class ClienteDTO
    {
        [Key]
        public int ClienteId { get; set; }

        [StringLength(100)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Senha { get; set; } = null!;

        [StringLength(20)]
        public string? Telefone { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DataNascimento { get; set; }

    }
}