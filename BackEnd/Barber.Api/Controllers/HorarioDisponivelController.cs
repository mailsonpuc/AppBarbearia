
using Barber.Api.DTOS;
using Barber.Api.DTOS.Mappings;
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
        public ActionResult<IEnumerable<HorarioDisponivelDTO>> Get()
        {

            var horarios = _uof.HorarioDisponivelRepository.GetAll();
            if (horarios is null)
            {
                return NotFound("horarios Não encontrado");
            }

            var horarioDisponielDto = horarios.ToHorarioDisponivelDTOList();

            return Ok(horarioDisponielDto);



        }





        [HttpGet("{id:int}", Name = "ObterHorarios")]
        public ActionResult<HorarioDisponivelDTO> Get(int id)
        {

            var horario = _uof.HorarioDisponivelRepository.Get(h => h.HorarioId == id);
            if (horario is null)
            {
                return NotFound($"horario com id= {id} não encontrado");
            }

            var horarioDto = horario.ToHorarioDisponivelDTO();

            return Ok(horarioDto);


        }







        [HttpPost]
        public ActionResult Post(HorarioDisponivelDTO horarioDto)
        {
            if (horarioDto is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            var horario = horarioDto.ToHorarioDisponivel();

            var horarioCriado = _uof.HorarioDisponivelRepository.Create(horario);
            _uof.Commit();


            var novoHorarioDto = horarioCriado.ToHorarioDisponivelDTO();

            return new CreatedAtRouteResult("ObterHorarios",
            new { id = novoHorarioDto.HorarioId }, novoHorarioDto);

        }




        [HttpPut("{id:int}")]
        public ActionResult<HorarioDisponivelDTO> Put(int id, HorarioDisponivelDTO horarioDto)
        {
            if (id != horarioDto.HorarioId)
            {
                return BadRequest("Não encontrado");
            }


            var horario = horarioDto.ToHorarioDisponivel();

            var horarioAtualizado = _uof.HorarioDisponivelRepository.Update(horario);
            _uof.Commit();
            //return NoContent();


            var horarioAtualizadoDto = horarioAtualizado.ToHorarioDisponivelDTO();

            return Ok(horarioAtualizadoDto);

        }



        [HttpDelete("{id:int}")]
        public ActionResult<HorarioDisponivelDTO> Delete(int id)
        {
            var horario = _uof.HorarioDisponivelRepository.Get(h => h.HorarioId == id);
            if (horario is null)
            {
                return NotFound($"horario com id= {id} não Localizada...");

            }



            var horarioExcluido = _uof.HorarioDisponivelRepository.Delete(horario);
            _uof.Commit();


            var horarioExcluidaDto = horarioExcluido.ToHorarioDisponivelDTO();

            return Ok(horarioExcluidaDto);

        }







    }
}