

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barber.Api.DTOS
{
    public class AvaliacaoDTO
    {
        public int AvaliacaoId { get; set; }

        public int? Nota { get; set; }

        [StringLength(300)]
        public string? Comentario { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Data { get; set; }

        public int AgendamentoId { get; set; }

    }
}