
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfereceController : ControllerBase
    {
        //usando o repository
        private readonly IRepository<Oferece> _repository;

        //construtor
        public OfereceController(IRepository<Oferece> repository)
        {
            //injetendo a dependencia
            _repository = repository;
        }




        [HttpGet]
        public ActionResult<IEnumerable<Oferece>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var ofereces = _repository.GetAll();
                if (ofereces is null)
                {
                    return NotFound("ofereces Não encontrado");
                }

                return Ok(ofereces);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpGet("{id:int}", Name = "ObterOferece")]
        public ActionResult<Oferece> Get(int id)
        {
            try
            {
                var oferece = _repository.Get(o => o.ServicoId == id);
                if (oferece is null)
                {
                    return NotFound($"oferece com id= {id} não encontrado");
                }

                return Ok(oferece);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }




        [HttpPost]
        public ActionResult Post(Oferece oferece)
        {
            if (oferece is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            var ofereceCriado = _repository.Create(oferece);

            return new CreatedAtRouteResult("ObterOferece",
            new { id = ofereceCriado.ServicoId }, ofereceCriado);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Oferece oferece)
        {
            if (id != oferece.ServicoId)
            {
                return BadRequest("Não encontrado");
            }

            _repository.Update(oferece);

            //return NoContent();
            return Ok(oferece);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var oferece = _repository.Get(o => o.ServicoId == id);
            if (oferece is null)
            {
                return NotFound($"oferece com id= {id} não Localizada...");

            }




            var ofereceExcluido = _repository.Delete(oferece);
            return Ok(ofereceExcluido);

        }





    }
}