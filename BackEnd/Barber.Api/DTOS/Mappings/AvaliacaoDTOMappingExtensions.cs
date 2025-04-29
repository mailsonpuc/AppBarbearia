

using Barber.Api.Models;

namespace Barber.Api.DTOS.Mappings
{
    public static class AvaliacaoDTOMappingExtensions
    {
        public static AvaliacaoDTO? ToAvaliacaoDTO(this Avaliacao avaliacao)
        {
            if (avaliacao is null)
            {
                return null;
            }

            return new AvaliacaoDTO
            {
                AvaliacaoId = avaliacao.AvaliacaoId,
                Nota = avaliacao.Nota,
                Comentario = avaliacao.Comentario,
                Data = avaliacao.Data,
                AgendamentoId = avaliacao.AgendamentoId

            };
        }





        public static Avaliacao? ToAvaliacao(this AvaliacaoDTO avaliacaoDto)
        {
            if (avaliacaoDto is null)
            {
                return null;
            }

            return new Avaliacao
            {
                AvaliacaoId = avaliacaoDto.AvaliacaoId,
                Nota = avaliacaoDto.Nota,
                Comentario = avaliacaoDto.Comentario,
                Data = avaliacaoDto.Data,
                AgendamentoId = avaliacaoDto.AgendamentoId
            };
        }





        public static IEnumerable<AvaliacaoDTO> ToAvaliacaoDTOList(this IEnumerable<AvaliacaoDTO> avaliacaos)
        {

            if (avaliacaos is null || !avaliacaos.Any())
            {
                return new List<AvaliacaoDTO>();
            }


            return avaliacaos.Select(avaliacao => new AvaliacaoDTO
            {
                AvaliacaoId = avaliacao.AvaliacaoId,
                Nota = avaliacao.Nota,
                Comentario = avaliacao.Comentario,
                Data = avaliacao.Data,
                AgendamentoId = avaliacao.AgendamentoId

            }).ToList();
        }




    }
}