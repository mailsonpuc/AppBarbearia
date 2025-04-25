
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Barber.Api.Models
{
    public class HistoricoCorte
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdHistorico { get; set; }

        public string? Foto { get; set; }

        public string? Observacoes { get; set; }

        [ForeignKey("Agendamento")]
        public int IdAgendamento { get; set; }
         //ignorando json
        [JsonIgnore]
        public Agendamento Agendamento { get; set; } = null!;
    }
}