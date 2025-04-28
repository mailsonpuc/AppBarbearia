

using Barber.Api.Context;
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;

namespace Barber.Api.Repositories
{
    public class HistoricoCorteRepository : Repository<HistoricoCorte>, IHistoricoCorteRepository
    {
        public HistoricoCorteRepository(AppDbContext context) : base(context)
        {
        }
    }
}