

using Barber.Api.DTOS;
using Barber.Api.DTOS.Mappings;
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Barber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoricoCorteController : ControllerBase
    {
        //usando o repository

        private readonly IUnitOfWork _uof;

        private readonly ILogger<HistoricoCorteController> _logger;

        public HistoricoCorteController(IUnitOfWork uof,
        ILogger<HistoricoCorteController> logger)
        {
            _uof = uof;
            _logger = logger;
        }








        [HttpGet]
        public ActionResult<IEnumerable<HistoricoCorteDTO>> Get()
        {

            var historicos = _uof.HistoricoCorteRepository.GetAll();
            if (historicos is null)
            {
                return NotFound("historicos Não encontrado");
            }

            var historicoDto = historicos.ToHistoricoCorteDTOList();

            return Ok(historicoDto);




        }





        [HttpGet("{id:int}", Name = "Obterhistorico")]
        public ActionResult<HistoricoCorteDTO> Get(int id)
        {

            var historico = _uof.HistoricoCorteRepository.Get(h => h.HistoricoId == id);
            if (historico is null)
            {
                _logger.LogError($"historico com id= {id} não encontrada...");
                return NotFound($"historico com id= {id} não encontrado");
            }


            var historicoDto = historico.ToHistoricoCorteDTO();

            return Ok(historicoDto);


        }




        [HttpPost]
        public ActionResult<HistoricoCorteDTO> Post(HistoricoCorteDTO historicoDto)
        {
            if (historicoDto is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }


            var historico = historicoDto.ToHistoricoCorte();
            var historicoCriado = _uof.HistoricoCorteRepository.Create(historico);
            _uof.Commit();

            var novoHistoricoDto = historicoCriado.ToHistoricoCorteDTO();

            return new CreatedAtRouteResult("Obterhistorico",
            new { id = novoHistoricoDto.HistoricoId }, novoHistoricoDto);

        }





        [HttpPut("{id:int}")]
        public ActionResult<HistoricoCorteDTO> Put(int id, HistoricoCorteDTO historicoDto)
        {
            if (id != historicoDto.HistoricoId)
            {
                return BadRequest("Não encontrado");

            }

            var historico = historicoDto.ToHistoricoCorte();

            var historicoAtualizado = _uof.HistoricoCorteRepository.Update(historico);
            _uof.Commit();


            var historicoAtualizadoDto = historicoAtualizado.ToHistoricoCorteDTO();

            return Ok(historicoAtualizadoDto);

        }





        [HttpDelete("{id:int}")]
        public ActionResult<HistoricoCorteDTO> Delete(int id)
        {
            var historico = _uof.HistoricoCorteRepository.Get(h => h.HistoricoId == id);
            if (historico is null)
            {
                return NotFound($"historico com id= {id} não Localizada...");

            }


            var historicoExcluido = _uof.HistoricoCorteRepository.Delete(historico);
            _uof.Commit();


            var historicoExcluidoDto = historicoExcluido.ToHistoricoCorteDTO();

            return Ok(historicoExcluidoDto);
        }











    }
}