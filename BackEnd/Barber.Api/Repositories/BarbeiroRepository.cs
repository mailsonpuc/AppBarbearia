
using Barber.Api.Context;
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;

namespace Barber.Api.Repositories
{
    public class BarbeiroRepository : Repository<Barbeiro>, IBarbeiroRepository
    {
        public BarbeiroRepository(AppDbContext context) : base(context)
        {
        }
    }
}