
using Barber.Api.Context;
using Barber.Api.Models;
using Barber.Api.Pagination;
using Barber.Api.Repositories.Interfaces;

namespace Barber.Api.Repositories
{
    public class AgendamentoRepository : Repository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(AppDbContext context) : base(context)
        {
        }



        public IEnumerable<Agendamento> GetAgendamentos(AgendamentosParameters agendamentosParams)
        {
            return GetAll()
            .OrderBy(p => p.AgendamentoId)
            .Skip((agendamentosParams.PageNumber - 1) * agendamentosParams.PageSize)
            .Take(agendamentosParams.PageSize).ToList();

        }




    }
}