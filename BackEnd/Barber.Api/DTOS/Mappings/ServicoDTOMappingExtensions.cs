
using Barber.Api.Models;

namespace Barber.Api.DTOS.Mappings
{
    public static class ServicoDTOMappingExtensions
    {
        public static ServicoDTO? ToServicoDTO(this Servico servico)
        {
            if (servico is null)
            {
                return null;
            }

            return new ServicoDTO
            {
                ServicoId = servico.ServicoId,
                Nome = servico.Nome,
                Descricao = servico.Descricao
            };
        }






        public static Servico? ToServico(this ServicoDTO servicoDto)
        {
            if (servicoDto is null)
            {
                return null;
            }

            return new Servico
            {
                ServicoId = servicoDto.ServicoId,
                Nome = servicoDto.Nome,
                Descricao = servicoDto.Descricao
            };
        }











        public static IEnumerable<ServicoDTO> ToServicoDTOList(this IEnumerable<Servico> servicos)
        {

            if (servicos is null || !servicos.Any())
            {
                return new List<ServicoDTO>();
            }


            return servicos.Select(servico => new ServicoDTO
            {
                ServicoId = servico.ServicoId,
                Nome = servico.Nome,
                Descricao = servico.Descricao
            }).ToList();



















        }
    }
}