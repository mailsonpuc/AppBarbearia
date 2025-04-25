
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Barber.Api.Models
{
    public class Agendamento
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgendamentoId { get; set; }

        [StringLength(100)]
        public string? Status { get; set; }

        public bool? LembreteEnviado { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
         //ignorando json
        //[JsonIgnore]
        public Cliente Cliente { get; set; } = null!;

        [ForeignKey("Servico")]
        public int ServicoId { get; set; }
         //ignorando json
        //[JsonIgnore]
        public Servico Servico { get; set; } = null!;

       
        [ForeignKey("HorarioDisponivel")]
        public int HorarioId { get; set; }
         //ignorando json
        //[JsonIgnore]
        public HorarioDisponivel HorarioDisponivel { get; set; } = null!;
    }
}

