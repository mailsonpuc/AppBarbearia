
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Barber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicoController : ControllerBase
    {
        //usando o repository
        private readonly IRepository<Servico> _repository;

        //construtor
        public ServicoController(IRepository<Servico> repository)
        {
            //injetendo a dependencia
            _repository = repository;
        }






        [HttpGet]
        public ActionResult<IEnumerable<Servico>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var servicos = _repository.GetAll();
                if (servicos is null)
                {
                    return NotFound("servicos Não encontrado");
                }

                return Ok(servicos);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }






        [HttpGet("{id:int}", Name = "ObterServico")]
        public ActionResult<Servico> Get(int id)
        {
            try
            {
                var servico = _repository.Get(s => s.ServicoId == id);
                if (servico is null)
                {
                    return NotFound($"servico com id= {id} não encontrado");
                }

                return Ok(servico);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpPost]
        public ActionResult Post(Servico servico)
        {
            if (servico is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            var servicoCriado = _repository.Create(servico);

            return new CreatedAtRouteResult("ObterServico",
            new { id = servicoCriado.ServicoId }, servicoCriado);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Servico servico)
        {
            if (id != servico.ServicoId)
            {
                return BadRequest("Não encontrado");
            }

            _repository.Update(servico);

            //return NoContent();
            return Ok(servico);

        }




        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var servico = _repository.Get(c => c.ServicoId == id);
            if (servico is null)
            {
                return NotFound($"servico com id= {id} não Localizada...");

            }


            var servicoExcluida = _repository.Delete(servico);
            return Ok(servicoExcluida);

        }





    }
}