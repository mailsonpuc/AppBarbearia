

using System.ComponentModel.DataAnnotations;

namespace Barber.Api.DTOS
{
    public class ServicoDTO
    {
        
        public int ServicoId { get; set; }

        [StringLength(100)]
        public string? Nome { get; set; }


        [StringLength(300)]
        public string? Descricao { get; set; }
    }
}