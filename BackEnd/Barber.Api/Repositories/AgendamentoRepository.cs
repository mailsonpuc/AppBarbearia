
using Barber.Api.Context;
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;

namespace Barber.Api.Repositories
{
    public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(AppDbContext context) : base(context)
        {
        }
    }
}