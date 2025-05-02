

using Barber.Api.Models;
using Barber.Api.Pagination;

namespace Barber.Api.Repositories.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {

        PagedList<Cliente> GetClientesFiltroNome(ClienteFiltroNome clienteFiltroNome);

    }
}