

using Barber.Api.Models;

namespace Barber.Api.DTOS.Mappings
{
    public static class HistoricoCorteDTOMappingExtensions
    {
        public static HistoricoCorteDTO? ToHistoricoCorteDTO(this HistoricoCorte historicoCorte)
        {
            if (historicoCorte is null)
            {
                return null;
            }

            return new HistoricoCorteDTO
            {
                HistoricoId = historicoCorte.HistoricoId,
                Foto = historicoCorte.Foto,
                Observacoes = historicoCorte.Observacoes,
                AgendamentoId = historicoCorte.AgendamentoId

            };
        }




     

        public static HistoricoCorte? ToHistoricoCorte(this HistoricoCorteDTO historicoCorteDto)
        {
            if (historicoCorteDto is null)
            {
                return null;
            }

            return new HistoricoCorte
            {
                HistoricoId = historicoCorteDto.HistoricoId,
                Foto = historicoCorteDto.Foto,
                Observacoes = historicoCorteDto.Observacoes,
                AgendamentoId = historicoCorteDto.AgendamentoId
            };
        }






    



        public static IEnumerable<HistoricoCorteDTO> ToHistoricoCorteDTOList(this IEnumerable<HistoricoCorte> historicoCortes)
        {

            if (historicoCortes is null || !historicoCortes.Any())
            {
                return new List<HistoricoCorteDTO>();
            }


            return historicoCortes.Select(historico => new HistoricoCorteDTO
            {
                HistoricoId = historico.HistoricoId,
                Foto = historico.Foto,
                Observacoes = historico.Observacoes,
                AgendamentoId = historico.AgendamentoId,

            }).ToList();



        }
    }
}