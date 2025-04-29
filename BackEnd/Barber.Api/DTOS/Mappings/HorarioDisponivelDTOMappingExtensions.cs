

using Barber.Api.Models;

namespace Barber.Api.DTOS.Mappings
{
    public static class HorarioDisponivelDTOMappingExtensions
    {
        public static HorarioDisponivelDTO? ToHorarioDisponivelDTO(this HorarioDisponivel horarioDisponivel)
        {
            if (horarioDisponivel is null)
            {
                return null;
            }

            return new HorarioDisponivelDTO
            {
                HorarioId = horarioDisponivel.HorarioId,
                BarbeiroId = horarioDisponivel.BarbeiroId,
                Data = horarioDisponivel.Data,
                HoraInicio = horarioDisponivel.HoraInicio,
                HoraFim = horarioDisponivel.HoraFim
            };
        }




        public static HorarioDisponivel? ToHorarioDisponivel(this HorarioDisponivelDTO horarioDisponivelDto)
        {
            if (horarioDisponivelDto is null)
            {
                return null;
            }

            return new HorarioDisponivel
            {
                HorarioId = horarioDisponivelDto.HorarioId,
                BarbeiroId = horarioDisponivelDto.BarbeiroId,
                Data = horarioDisponivelDto.Data,
                HoraInicio = horarioDisponivelDto.HoraInicio,
                HoraFim = horarioDisponivelDto.HoraFim
            };
        }









        public static IEnumerable<HorarioDisponivelDTO> ToHorarioDisponivelDTOList(this IEnumerable<HorarioDisponivel> horarioDisponivels)
        {

            if (horarioDisponivels is null || !horarioDisponivels.Any())
            {
                return new List<HorarioDisponivelDTO>();
            }


            return horarioDisponivels.Select(horario => new HorarioDisponivelDTO
            {
                HorarioId = horario.HorarioId,
                BarbeiroId = horario.BarbeiroId,
                Data = horario.Data,
                HoraInicio = horario.HoraInicio,
                HoraFim = horario.HoraFim

            }).ToList();



        }

    }
}