
using Barber.Api.Context;
using Barber.Api.Repositories.Interfaces;

namespace Barber.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private IServicoRepository? _servicoRepo;
        private IOfereceRepository? _ofereceRepo;
        private IHorarioDisponivelRepository? _horarioDisponivelRepo;
        private IHistoricoCorteRepository? _historicoCorteRepo;
        private IClienteRepository? _clienteRepo;
        private IBarbeiroRepository? _barbeiroRepo;
        private IAvaliacaoRepository? _avaliacaoRepo;
        private IAgendamentoRepository? _agendamentoRepo;

        public AppDbContext _context;


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }





        public IServicoRepository ServicoRepository
        {
            get
            {
                return _servicoRepo = _servicoRepo ?? new ServicoRepository(_context);
            }
        }



        public IOfereceRepository OfereceRepository
        {
            get
            {
                return _ofereceRepo = _ofereceRepo ?? new OfereceRepository(_context);
            }
        }



        public IHorarioDisponivelRepository HorarioDisponivelRepository
        {
            get
            {
                return _horarioDisponivelRepo = _horarioDisponivelRepo ?? new HorarioDisponivelRepository(_context);
            }
        }



        public IHistoricoCorteRepository HistoricoCorteRepository
        {
            get
            {
                return _historicoCorteRepo = _historicoCorteRepo ?? new HistoricoCorteRepository(_context);
            }
        }



        public IClienteRepository ClienteRepository
        {
            get
            {
                return _clienteRepo = _clienteRepo ?? new ClienteRepository(_context);
            }
        }

        public IBarbeiroRepository BarbeiroRepository
        {
            get
            {
                return _barbeiroRepo = _barbeiroRepo ?? new BarbeiroRepository(_context);
            }
        }



        public IAvaliacaoRepository AvaliacaoRepository
        {
            get
            {
                return _avaliacaoRepo = _avaliacaoRepo ?? new AvaliacaoRepository(_context);
            }
        }



        public IAgendamentoRepository AgendamentoRepository
        {
            get
            {
                return _agendamentoRepo = _agendamentoRepo ?? new AgendamentoRepository(_context);
            }
        }



        public void Commit()
        {
            _context.SaveChanges();
        }



        public void Dispose()
        {
            _context.Dispose();
        }




        
    }

}