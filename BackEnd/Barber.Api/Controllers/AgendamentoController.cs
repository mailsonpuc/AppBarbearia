
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Barber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {

        private readonly IUnitOfWork _uof;

        private readonly ILogger<AgendamentoController> _logger;

        //construtor
        public AgendamentoController(IUnitOfWork uof,
        ILogger<AgendamentoController> logger)
        {
            _uof = uof;
            _logger = logger;
        }






        [HttpGet]
        public ActionResult<IEnumerable<Agendamento>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var agendamentos = _uof.AgendamentoRepository.GetAll();
                if (agendamentos is null)
                {
                    return NotFound("agendamentos Não encontrado");
                }

                return Ok(agendamentos);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpGet("{id:int}", Name = "ObterAgendamentos")]
        public ActionResult<Oferece> Get(int id)
        {
            try
            {
                var ag = _uof.AgendamentoRepository.Get(g => g.AgendamentoId == id);
                if (ag is null)
                {
                    return NotFound($"agendamento com id= {id} não encontrado");
                }

                return Ok(ag);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }




        [HttpPost]
        public ActionResult Post(Agendamento ag)
        {
            if (ag is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }


            var agendamentoCriado = _uof.AgendamentoRepository.Create(ag);
            _uof.Commit();

            return new CreatedAtRouteResult("ObterAgendamentos",
            new { id = agendamentoCriado.AgendamentoId }, agendamentoCriado);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Agendamento ag)
        {
            if (id != ag.AgendamentoId)
            {
                return BadRequest("Não encontrado");
            }


            _uof.AgendamentoRepository.Update(ag);
            _uof.Commit();
            return Ok(ag);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var ag = _uof.AgendamentoRepository.Get(s => s.AgendamentoId == id);
            if (ag is null)
            {
                return NotFound($"agendamento com id= {id} não Localizada...");

            }

            var agendamentoExcluido = _uof.AgendamentoRepository.Delete(ag);
            _uof.Commit();

            return Ok(agendamentoExcluido);

        }



    }
}