

using Barber.Api.Models;

namespace Barber.Api.DTOS.Mappings
{
    public static class OfereceDTOMappingExtensions
    {
        public static OfereceDTO? ToOfereceDTO(this Oferece oferece)
        {
            if (oferece is null)
            {
                return null;
            }

            return new OfereceDTO
            {
                BarbeiroId = oferece.BarbeiroId,
                ServicoId = oferece.ServicoId,
                Preco = oferece.Preco,
                Duracao = oferece.Duracao
            };
        }




        public static Oferece? ToOferece(this OfereceDTO ofereceDto)
        {
            if (ofereceDto is null)
            {
                return null;
            }

            return new Oferece
            {
                BarbeiroId = ofereceDto.BarbeiroId,
                ServicoId = ofereceDto.ServicoId,
                Preco = ofereceDto.Preco,
                Duracao = ofereceDto.Duracao
            };
        }









        public static IEnumerable<OfereceDTO> ToOfereceDTOList(this IEnumerable<Oferece> ofereces)
        {

            if (ofereces is null || !ofereces.Any())
            {
                return new List<OfereceDTO>();
            }


            return ofereces.Select(oferece => new OfereceDTO
            {
                BarbeiroId = oferece.BarbeiroId,
                ServicoId = oferece.ServicoId,
                Preco = oferece.Preco,
                Duracao = oferece.Duracao
            }).ToList();






        }
    }
}