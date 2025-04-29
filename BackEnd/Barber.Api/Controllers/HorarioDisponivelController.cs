
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Barber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorarioDisponivelController : ControllerBase
    {
        //usando o repository

        private readonly IUnitOfWork _uof;
        private readonly ILogger<HorarioDisponivelController> _logger;

        public HorarioDisponivelController(IUnitOfWork uof,
        ILogger<HorarioDisponivelController> logger)
        {
            _uof = uof;
            _logger = logger;
        }




        [HttpGet]
        public ActionResult<IEnumerable<HorarioDisponivel>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var horarios = _uof.HorarioDisponivelRepository.GetAll();
                if (horarios is null)
                {
                    return NotFound("horarios Não encontrado");
                }

                return Ok(horarios);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpGet("{id:int}", Name = "ObterHorarios")]
        public ActionResult<Oferece> Get(int id)
        {
            try
            {
                var horario = _uof.HorarioDisponivelRepository.Get(h => h.HorarioId == id);
                if (horario is null)
                {
                    return NotFound($"horario com id= {id} não encontrado");
                }

                return Ok(horario);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }







        [HttpPost]
        public ActionResult Post(HorarioDisponivel horario)
        {
            if (horario is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            var horarioCriado = _uof.HorarioDisponivelRepository.Create(horario);
            _uof.Commit();

            return new CreatedAtRouteResult("ObterHorarios",
            new { id = horarioCriado.HorarioId }, horarioCriado);

        }




        [HttpPut("{id:int}")]
        public ActionResult Put(int id, HorarioDisponivel horario)
        {
            if (id != horario.HorarioId)
            {
                return BadRequest("Não encontrado");
            }

            _uof.HorarioDisponivelRepository.Update(horario);
            _uof.Commit();
            //return NoContent();
            return Ok(horario);

        }



        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var horario = _uof.HorarioDisponivelRepository.Get(h => h.HorarioId == id);
            if (horario is null)
            {
                return NotFound($"horario com id= {id} não Localizada...");

            }



            var horarioExcluido = _uof.HorarioDisponivelRepository.Delete(horario);
            _uof.Commit();
            return Ok(horarioExcluido);
        }







    }
}