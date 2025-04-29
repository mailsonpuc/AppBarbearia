

using System.ComponentModel.DataAnnotations;

namespace Barber.Api.DTOS
{
    public class BarbeiroDTO
    {
        public int BarbeiroId { get; set; }

        [StringLength(100)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Senha { get; set; } = null!;

        [StringLength(20)]
        public string? Telefone { get; set; }

    }
}