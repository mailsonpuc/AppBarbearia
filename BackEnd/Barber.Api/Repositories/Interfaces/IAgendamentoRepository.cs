
using Barber.Api.Models;
using Barber.Api.Pagination;

namespace Barber.Api.Repositories.Interfaces
{
    public interface IAgendamentoRepository : IRepository<Agendamento>
    {

        IEnumerable<Agendamento> GetAgendamentos(AgendamentosParameters agendamentosParams);

    }
}