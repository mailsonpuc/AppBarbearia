
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Barber.Api.Models
{
    public class Servico
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServicoId { get; set; }

        [StringLength(100)]
        public string? Nome { get; set; }


        [StringLength(300)]
        public string? Descricao { get; set; }
    }
}