

namespace Barber.Api.Repositories.Interfaces
{
    public interface IUnitOfWork 
    {
        IServicoRepository ServicoRepository { get; }
        IOfereceRepository OfereceRepository { get; }
        IHorarioDisponivelRepository HorarioDisponivelRepository { get; }
        IHistoricoCorteRepository HistoricoCorteRepository { get; }
        IClienteRepository ClienteRepository { get; }
        IBarbeiroRepository BarbeiroRepository { get; }
        IAvaliacaoRepository AvaliacaoRepository { get; }
        IAgendamentoRepository AgendamentoRepository { get; }
        

        void Commit();
    }
}