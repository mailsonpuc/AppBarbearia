

using Barber.Api.Models;

namespace Barber.Api.DTOS.Mappings
{
    public static class ClienteDTOMappingExtensions
    {
        public static ClienteDTO? ToClienteDTO(this Cliente cliente)
        {
            if (cliente is null)
            {
                return null;
            }

            return new ClienteDTO
            {
                ClienteId = cliente.ClienteId,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Senha = cliente.Senha,
                Telefone = cliente.Telefone,
                DataNascimento = cliente.DataNascimento

            };
        }




        public static Cliente? ToCliente(this ClienteDTO clienteDto)
        {
            if (clienteDto is null)
            {
                return null;
            }

            return new Cliente
            {
                ClienteId = clienteDto.ClienteId,
                Nome = clienteDto.Nome,
                Email = clienteDto.Email,
                Senha = clienteDto.Senha,
                Telefone = clienteDto.Telefone,
                DataNascimento = clienteDto.DataNascimento
            };
        }











        public static IEnumerable<ClienteDTO> ToClienteDTOList(this IEnumerable<Cliente> clientes)
        {

            if (clientes is null || !clientes.Any())
            {
                return new List<ClienteDTO>();
            }


            return clientes.Select(cliente => new ClienteDTO
            {
                ClienteId = cliente.ClienteId,
                Nome = cliente.Nome,
                Email = cliente.Email,
                Senha = cliente.Senha,
                Telefone = cliente.Telefone,
                DataNascimento = cliente.DataNascimento

            }).ToList();



        }
    }
}