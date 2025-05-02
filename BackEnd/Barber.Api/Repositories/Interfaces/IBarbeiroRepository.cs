
using Barber.Api.Models;
using Barber.Api.Pagination;

namespace Barber.Api.Repositories.Interfaces
{
    public interface IBarbeiroRepository : IRepository<Barbeiro>
    {
        PagedList<Barbeiro> GetBarbeiros(BarbeirosParameters barbeirosParameters);
        
    }
}