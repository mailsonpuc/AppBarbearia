

namespace Barber.Api.DTOS
{
    public class HistoricoCorteDTO
    {
        public int HistoricoId { get; set; }

        public string? Foto { get; set; }

        public string? Observacoes { get; set; }

        public int AgendamentoId { get; set; }

    }
}