
using Barber.Api.DTOS;
using Barber.Api.DTOS.Mappings;
using Barber.Api.Models;
using Barber.Api.Pagination;
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

            var agendamentos = _uof.AgendamentoRepository.GetAll();
            if (agendamentos is null)
            {
                _logger.LogWarning($"não encontrada...");
                return NotFound("agendamentos Não encontrado");
            }

            var agendamentosDto = agendamentos.ToAgendamentoDTOList();
            _logger.LogCritical("Usario Esta Puxando tudo, pode travar o servidor se tiver muitos dados");


            return Ok(agendamentosDto);



        }





        [HttpGet("pagination")]
        public ActionResult<IEnumerable<AgendamentoDTO>> Get([FromQuery] AgendamentosParameters agendamentosParameters)
        {
            // Obtém a lista de agendamentos do repositório
            var agendamentos = _uof.AgendamentoRepository.GetAgendamentos(agendamentosParameters);

            // Converte a lista de Agendamentos em uma lista de AgendamentoDTO usando a extensão
            var agendamentosDto = agendamentos.ToAgendamentoDTOList();
            _logger.LogWarning("Usuario esta usando a pagination");

            return Ok(agendamentosDto);
        }







        [HttpGet("{id:int}", Name = "ObterAgendamentos")]
        public ActionResult<AgendamentoDTO> Get(int id)
        {

            var agendamento = _uof.AgendamentoRepository.Get(g => g.AgendamentoId == id);
            if (agendamento is null)
            {

                _logger.LogWarning($"agendamento com id= {id} não encontrada...");
                return NotFound($"agendamento com id= {id} não encontrado");
            }


            var agendamentoDto = agendamento.ToAgendamentoDTO();

            return Ok(agendamentoDto);




        }




        [HttpPost]
        public ActionResult<AgendamentoDTO> Post(AgendamentoDTO agendamentoDto)
        {
            if (agendamentoDto is null)
            {

                _logger.LogWarning($"Dados Invalidos..");
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

                _logger.LogWarning($" não encontrada...");
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

                _logger.LogWarning($"Agendamento com id= {id} não encontrada...");
                return NotFound($"agendamento com id= {id} não Localizada...");

            }

            var agendamentoExcluido = _uof.AgendamentoRepository.Delete(agendamento);
            _uof.Commit();

            var agendamentoExcluidoDto = agendamentoExcluido.ToAgendamentoDTO();

            return Ok(agendamentoExcluidoDto);

        }



    }
}