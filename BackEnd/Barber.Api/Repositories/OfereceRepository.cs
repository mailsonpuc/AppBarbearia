
using Barber.Api.Context;
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;

namespace Barber.Api.Repositories
{
    public class OfereceRepository : Repository<Oferece>, IOfereceRepository
    {
        public OfereceRepository(AppDbContext context) : base(context)
        {
        }

    }
}