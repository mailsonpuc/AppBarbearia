

using Barber.Api.Models;

namespace Barber.Api.DTOS.Mappings
{
    public static class BarbeiroDTOMappingExtensions
    {

        public static BarbeiroDTO? ToBarbeiroDTO(this Barbeiro barbeiro)
        {
            if (barbeiro is null)
            {
                return null;
            }

            return new BarbeiroDTO
            {
                BarbeiroId = barbeiro.BarbeiroId,
                Nome = barbeiro.Nome,
                Email = barbeiro.Email,
                Senha = barbeiro.Senha,
                Telefone = barbeiro.Telefone

            };
        }




        public static Barbeiro? ToBarbeiro(this BarbeiroDTO barbeiroDto)
        {
            if (barbeiroDto is null)
            {
                return null;
            }

            return new Barbeiro
            {
                BarbeiroId = barbeiroDto.BarbeiroId,
                Nome = barbeiroDto.Nome,
                Email = barbeiroDto.Email,
                Senha = barbeiroDto.Senha,
                Telefone = barbeiroDto.Telefone
            };
        }









        public static IEnumerable<BarbeiroDTO> ToBarbeiroDTOList(this IEnumerable<BarbeiroDTO> barbeiros)
        {

            if (barbeiros is null || !barbeiros.Any())
            {
                return new List<BarbeiroDTO>();
            }


            return barbeiros.Select(barbeiro => new BarbeiroDTO
            {
                BarbeiroId = barbeiro.BarbeiroId,
                Nome = barbeiro.Nome,
                Email = barbeiro.Email,
                Senha = barbeiro.Senha,
                Telefone = barbeiro.Telefone

            }).ToList();
        }
    }
}

