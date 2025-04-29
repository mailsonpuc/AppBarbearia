

using Barber.Api.Models;

namespace Barber.Api.DTOS.Mappings
{
    public static class AgendamentoDTOMappingExtensions
    {
        public static AgendamentoDTO? ToAgendamentoDTO(this Agendamento agendamento)
        {
            if (agendamento is null)
            {
                return null;
            }

            return new AgendamentoDTO
            {
                AgendamentoId = agendamento.AgendamentoId,
                Status = agendamento.Status,
                LembreteEnviado = agendamento.LembreteEnviado,
                ClienteId = agendamento.ClienteId,
                ServicoId = agendamento.ServicoId,
                HorarioId = agendamento.HorarioId
            };
        }







        public static Agendamento? ToAgendamento(this AgendamentoDTO agendamentoDto)
        {
            if (agendamentoDto is null)
            {
                return null;
            }

            return new Agendamento
            {
                AgendamentoId = agendamentoDto.AgendamentoId,
                Status = agendamentoDto.Status,
                LembreteEnviado = agendamentoDto.LembreteEnviado,
                ClienteId = agendamentoDto.ClienteId,
                ServicoId = agendamentoDto.ServicoId,
                HorarioId = agendamentoDto.HorarioId
            };
        }






        public static IEnumerable<AgendamentoDTO> ToAgendamentoDTOList(this IEnumerable<Agendamento> agendamentos)
        {

            if (agendamentos is null || !agendamentos.Any())
            {
                return new List<AgendamentoDTO>();
            }


            return agendamentos.Select(agendamento => new AgendamentoDTO
            {
                AgendamentoId = agendamento.AgendamentoId,
                Status = agendamento.Status,
                LembreteEnviado = agendamento.LembreteEnviado,
                ClienteId = agendamento.ClienteId,
                ServicoId = agendamento.ServicoId,
                HorarioId = agendamento.HorarioId

            }).ToList();





        }
    }
}