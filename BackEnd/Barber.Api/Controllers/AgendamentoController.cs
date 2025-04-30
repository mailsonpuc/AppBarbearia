
using Barber.Api.DTOS;
using Barber.Api.DTOS.Mappings;
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
        public ActionResult<IEnumerable<AgendamentoDTO>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var agendamentos = _uof.AgendamentoRepository.GetAll();
                if (agendamentos is null)
                {
                    return NotFound("agendamentos Não encontrado");
                }

                var agendamentosDto = agendamentos.ToAgendamentoDTOList();

                return Ok(agendamentosDto);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpGet("{id:int}", Name = "ObterAgendamentos")]
        public ActionResult<AgendamentoDTO> Get(int id)
        {
            try
            {
                var agendamento = _uof.AgendamentoRepository.Get(g => g.AgendamentoId == id);
                if (agendamento is null)
                {
                    return NotFound($"agendamento com id= {id} não encontrado");
                }


                var agendamentoDto = agendamento.ToAgendamentoDTO();

                return Ok(agendamentoDto);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }




        [HttpPost]
        public ActionResult<AgendamentoDTO> Post(AgendamentoDTO agendamentoDto)
        {
            if (agendamentoDto is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }


            var agendamento = agendamentoDto.ToAgendamento();
            var agendamentoCriado = _uof.AgendamentoRepository.Create(agendamento);
            _uof.Commit();

            var novoAgendamentoDto = agendamentoCriado.ToAgendamentoDTO();

            return new CreatedAtRouteResult("ObterAgendamentos",
            new { id = novoAgendamentoDto.AgendamentoId }, novoAgendamentoDto);

        }





        [HttpPut("{id:int}")]
        public ActionResult<AgendamentoDTO> Put(int id, AgendamentoDTO agendamentoDto)
        {
            if (id != agendamentoDto.AgendamentoId)
            {
                return BadRequest("Não encontrado");
            }


            var agendamento = agendamentoDto.ToAgendamento();
            var agendamentoAtualizado = _uof.AgendamentoRepository.Update(agendamento);
            _uof.Commit();

            var agendamentoAtualizadoDto = agendamentoAtualizado.ToAgendamentoDTO();

            return Ok(agendamentoAtualizadoDto);

        }





        [HttpDelete("{id:int}")]
        public ActionResult<AgendamentoDTO> Delete(int id)
        {
            var agendamento = _uof.AgendamentoRepository.Get(s => s.AgendamentoId == id);
            if (agendamento is null)
            {
                return NotFound($"agendamento com id= {id} não Localizada...");

            }

            var agendamentoExcluido = _uof.AgendamentoRepository.Delete(agendamento);
            _uof.Commit();

            var agendamentoExcluidoDto = agendamentoExcluido.ToAgendamentoDTO();

            return Ok(agendamentoExcluidoDto);

        }



    }
}