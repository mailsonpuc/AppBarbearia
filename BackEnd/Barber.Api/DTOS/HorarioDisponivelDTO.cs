

namespace Barber.Api.DTOS
{
    public class HorarioDisponivelDTO
    {
        public int HorarioId { get; set; }

       
        public int BarbeiroId { get; set; }
       

        public DateTime? Data { get; set; }

     
        public TimeSpan? HoraInicio { get; set; }

        public TimeSpan? HoraFim { get; set; }
    }
}
