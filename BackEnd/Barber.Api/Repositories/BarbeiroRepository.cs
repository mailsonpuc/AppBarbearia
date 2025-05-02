
using Barber.Api.Context;
using Barber.Api.Models;
using Barber.Api.Pagination;
using Barber.Api.Repositories.Interfaces;

namespace Barber.Api.Repositories
{
    public class BarbeiroRepository : Repository<Barbeiro>, IBarbeiroRepository
    {
        public BarbeiroRepository(AppDbContext context) : base(context)
        {
        }


        public PagedList<Barbeiro> GetBarbeiros(BarbeirosParameters barbeirosParameters)
        {
            var barbeiros = GetAll().OrderBy(p => p.BarbeiroId).AsQueryable();
            var barbeirosOrdenados = PagedList<Barbeiro>.ToPagedList(barbeiros,
                        barbeirosParameters.PageNumber, barbeirosParameters.PageSize);

            return barbeirosOrdenados;
        }







    }
}