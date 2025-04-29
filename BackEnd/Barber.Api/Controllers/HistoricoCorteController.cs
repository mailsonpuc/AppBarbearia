

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
        public ActionResult<IEnumerable<HistoricoCorte>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var historicos = _uof.HistoricoCorteRepository.GetAll();
                if (historicos is null)
                {
                    return NotFound("historicos Não encontrado");
                }

                return Ok(historicos);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpGet("{id:int}", Name = "Obterhistorico")]
        public ActionResult<HistoricoCorte> Get(int id)
        {
            try
            {
                var historico = _uof.HistoricoCorteRepository.Get(h => h.HistoricoId == id);
                if (historico is null)
                {
                    return NotFound($"historico com id= {id} não encontrado");
                }

                return Ok(historico);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }




        [HttpPost]
        public ActionResult Post(HistoricoCorte historico)
        {
            if (historico is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            var historicoCriado = _uof.HistoricoCorteRepository.Create(historico);
            _uof.Commit();

            return new CreatedAtRouteResult("Obterhistorico",
            new { id = historicoCriado.HistoricoId }, historicoCriado);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, HistoricoCorte historico)
        {
            if (id != historico.HistoricoId)
            {
                return BadRequest("Não encontrado");
            }
            _uof.HistoricoCorteRepository.Update(historico);
            _uof.Commit();
            return Ok(historico);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var historico = _uof.HistoricoCorteRepository.Get(h => h.HistoricoId == id);
            if (historico is null)
            {
                return NotFound($"historico com id= {id} não Localizada...");

            }


            var historicoExcluido = _uof.HistoricoCorteRepository.Delete(historico);
            _uof.Commit();
            return Ok(historicoExcluido);
        }











    }
}