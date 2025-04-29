
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Barber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        //usando o repository
        private readonly IUnitOfWork _uof;

        private readonly ILogger<AvaliacaoController> _logger;


        public AvaliacaoController(IUnitOfWork uof,
        ILogger<AvaliacaoController> logger)
        {
            _uof = uof;
            _logger = logger;
        }



        [HttpGet]
        public ActionResult<IEnumerable<Avaliacao>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var avaliacoes = _uof.AvaliacaoRepository.GetAll();
                if (avaliacoes is null)
                {
                    return NotFound("avaliacoes Não encontrado");
                }

                return Ok(avaliacoes);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpGet("{id:int}", Name = "ObterAvaliacao")]
        public ActionResult<Oferece> Get(int id)
        {
            try
            {
                var av = _uof.AvaliacaoRepository.Get(a => a.AvaliacaoId == id);
                if (av is null)
                {
                    return NotFound($"avaliação com id= {id} não encontrado");
                }

                return Ok(av);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }




        [HttpPost]
        public ActionResult Post(Avaliacao av)
        {
            if (av is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }


            var avaliacaoCriado = _uof.AvaliacaoRepository.Create(av);
            _uof.Commit();

            return new CreatedAtRouteResult("ObterAvaliacao",
            new { id = avaliacaoCriado.AvaliacaoId }, avaliacaoCriado);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Avaliacao av)
        {
            if (id != av.AvaliacaoId)
            {
                return BadRequest("Não encontrado");
            }

            _uof.AvaliacaoRepository.Update(av);
            _uof.Commit();

            //return NoContent();
            return Ok(av);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var av = _uof.AvaliacaoRepository.Get(a => a.AvaliacaoId == id);
            if (av is null)
            {
                return NotFound($"avaliação com id= {id} não Localizada...");

            }



            var avaliacaoExcluido = _uof.AvaliacaoRepository.Delete(av);
            _uof.Commit();
            return Ok(avaliacaoExcluido);
        }





    }
}