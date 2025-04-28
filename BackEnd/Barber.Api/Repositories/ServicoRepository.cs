
using Barber.Api.Context;
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;

namespace Barber.Api.Repositories
{
    public class ServicoRepository : Repository<Servico>, IServicoRepository
    {
        public ServicoRepository(AppDbContext context) : base(context)
        {
        }
        

    }
}