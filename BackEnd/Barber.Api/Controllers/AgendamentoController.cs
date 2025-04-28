
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Barber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgendamentoController : ControllerBase
    {
        //usando o repository
        private readonly IRepository<Agendamento> _repository;

        //construtor
        public AgendamentoController(IRepository<Agendamento> repository)
        {
            //injetendo a dependencia
            _repository = repository;
        }




        [HttpGet]
        public ActionResult<IEnumerable<Agendamento>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var agendamentos = _repository.GetAll();
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
                var ag =  _repository.Get(g => g.AgendamentoId == id);
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


            var agendamentoCriado = _repository.Create(ag);

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


            _repository.Update(ag);
            return Ok(ag);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var ag = _repository.Get(s => s.AgendamentoId == id);
            if (ag is null)
            {
                return NotFound($"agendamento com id= {id} não Localizada...");

            }

            var agendamentoExcluido = _repository.Delete(ag);
            return Ok(agendamentoExcluido);

        }



    }
}