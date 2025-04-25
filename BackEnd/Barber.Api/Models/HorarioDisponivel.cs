

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Barber.Api.Models
{
    public class HorarioDisponivel
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HorarioId { get; set; }

        [ForeignKey("Barbeiro")]
        public int BarbeiroId { get; set; }
        //ignorando json
        [JsonIgnore]
        public Barbeiro Barbeiro { get; set; } = null!;

        [Column(TypeName = "date")]
        public DateTime? Data { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? HoraInicio { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? HoraFim { get; set; }
    }
}