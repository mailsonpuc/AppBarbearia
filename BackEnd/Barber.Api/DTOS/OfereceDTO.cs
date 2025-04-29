
using System.ComponentModel.DataAnnotations;

namespace Barber.Api.DTOS
{
    public class OfereceDTO
    {
        [Key]
        public int BarbeiroId { get; set; }

        [Key]
        public int ServicoId { get; set; }

        [Required]

        public decimal Preco { get; set; }

        [Required]
        public int Duracao { get; set; } // em minutos




    }
}