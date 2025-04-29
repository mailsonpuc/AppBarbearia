

using System.ComponentModel.DataAnnotations;

namespace Barber.Api.DTOS
{
    public class AgendamentoDTO
    {
        public int AgendamentoId { get; set; }

        [StringLength(100)]
        public string? Status { get; set; }

        public bool? LembreteEnviado { get; set; }


        public int ClienteId { get; set; }

        public int ServicoId { get; set; }


        public int HorarioId { get; set; }

    }
}