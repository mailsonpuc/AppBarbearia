

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Api.Models
{
    public class Barbeiro
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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