

using Barber.Api.Context;
using Barber.Api.Models;
using Barber.Api.Pagination;
using Barber.Api.Repositories.Interfaces;

namespace Barber.Api.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext context) : base(context)
        {
        }

        public PagedList<Cliente> GetClientesFiltroNome(ClienteFiltroNome clienteFiltroNome)
        {
            // throw new NotImplementedException();
            var clientes = GetAll().AsQueryable();
            if (!string.IsNullOrEmpty(clienteFiltroNome.Nome))
            {
                clientes = clientes.Where(c => c.Nome.Contains(clienteFiltroNome.Nome));
            }


            var clientesFiltrados = PagedList<Cliente>.ToPagedList(clientes,
                                     clienteFiltroNome.PageNumber, clienteFiltroNome.PageSize);

            return clientesFiltrados;


        }





    }
}