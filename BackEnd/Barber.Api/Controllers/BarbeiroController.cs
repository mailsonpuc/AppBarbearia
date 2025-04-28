
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

        private readonly IUnitOfWork _uof;

        public BarbeiroController(IUnitOfWork uof)
        {
            _uof = uof;
        }







        [HttpGet]
        public ActionResult<IEnumerable<Barbeiro>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var barbeiros = _uof.BarbeiroRepository.GetAll();
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
                var barbeiro = _uof.BarbeiroRepository.Get(b => b.BarbeiroId == id);
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

            var barbeiroCriado = _uof.BarbeiroRepository.Create(barbeiro);
            _uof.Commit();

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


            _uof.BarbeiroRepository.Update(barbeiro);
            _uof.Commit();
            return Ok(barbeiro);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var barbeiro = _uof.BarbeiroRepository.Get(x => x.BarbeiroId == id);
            if (barbeiro is null)
            {
                return NotFound($"barbeiro com id= {id} não Localizada...");

            }


            var barbeiroExcluido = _uof.BarbeiroRepository.Delete(barbeiro);
            _uof.Commit();
            return Ok(barbeiroExcluido);

        }














    }
}