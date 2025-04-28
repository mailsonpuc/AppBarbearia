

using Barber.Api.Context;
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;

namespace Barber.Api.Repositories
{
    public class HorarioDisponivelRepository : Repository<HorarioDisponivel>, IHorarioDisponivelRepository
    {
        public HorarioDisponivelRepository(AppDbContext context) : base(context)
        {
        }
    }
}