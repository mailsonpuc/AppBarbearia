
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Barber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarbeiroController : ControllerBase
    {
        //usando o repository
        private readonly IRepository<Barbeiro> _repository;

        //construtor
        public BarbeiroController(IRepository<Barbeiro> repository)
        {
            //injetendo a dependencia
            _repository = repository;
        }







        [HttpGet]
        public ActionResult<IEnumerable<Barbeiro>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var barbeiros = _repository.GetAll();
                if (barbeiros is null)
                {
                    return NotFound("barbeiros Não encontrado");
                }

                return Ok(barbeiros);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpGet("{id:int}", Name = "ObterBarbeiro")]
        public ActionResult<Barbeiro> Get(int id)
        {
            try
            {
                var barbeiro = _repository.Get(b => b.BarbeiroId == id);
                if (barbeiro is null)
                {
                    return NotFound($"barbeiro com id= {id} não encontrado");
                }

                return Ok(barbeiro);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }




        [HttpPost]
        public ActionResult Post(Barbeiro barbeiro)
        {
            if (barbeiro is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            var barbeiroCriado = _repository.Create(barbeiro);

            return new CreatedAtRouteResult("ObterBarbeiro",
            new { id = barbeiroCriado.BarbeiroId }, barbeiroCriado);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Barbeiro barbeiro)
        {
            if (id != barbeiro.BarbeiroId)
            {
                return BadRequest("Não encontrado");
            }


            _repository.Update(barbeiro);
            return Ok(barbeiro);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var barbeiro = _repository.Get(x => x.BarbeiroId == id);
            if (barbeiro is null)
            {
                return NotFound($"barbeiro com id= {id} não Localizada...");

            }


            var barbeiroExcluido = _repository.Delete(barbeiro);
            return Ok(barbeiroExcluido);

        }














    }
}