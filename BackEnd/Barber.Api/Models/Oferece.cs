
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Barber.Api.Models
{
    public class Oferece
    {
        [Key, Column(Order = 0)]
        public int BarbeiroId { get; set; }

        [Key, Column(Order = 1)]
        public int ServicoId { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [Required]
        public int Duracao { get; set; } // em minutos

        [ForeignKey("IdBarbeiro")]
         //ignorando json
        [JsonIgnore]
        public Barbeiro Barbeiro { get; set; } = null!;

        [ForeignKey("IdServico")]
         //ignorando json
        [JsonIgnore]
        public Servico Servico { get; set; } = null!;
    }
}