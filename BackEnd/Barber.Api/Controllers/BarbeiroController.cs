
using Barber.Api.DTOS;
using Barber.Api.DTOS.Mappings;
using Barber.Api.Models;
using Barber.Api.Pagination;
using Barber.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Barber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BarbeiroController : ControllerBase
    {
        //usando o repository

        private readonly IUnitOfWork _uof;

        private readonly ILogger<BarbeiroController> _logger;

        public BarbeiroController(IUnitOfWork uof,
        ILogger<BarbeiroController> logger)
        {
            _uof = uof;
            _logger = logger;
        }







        [HttpGet]
        public ActionResult<IEnumerable<BarbeiroDTO>> Get()
        {

            var barbeiros = _uof.BarbeiroRepository.GetAll();
            if (barbeiros is null)
            {
                return NotFound("barbeiros Não encontrado");
            }

            var barbeirosDto = barbeiros.ToBarbeiroDTOList();

            return Ok(barbeirosDto);




        }




   
        [HttpGet("pagination")]
        public ActionResult<IEnumerable<BarbeiroDTO>> Get([FromQuery] BarbeirosParameters barbeirosParameters)
        {
            var barbeiros = _uof.BarbeiroRepository.GetBarbeiros(barbeirosParameters);

            var metadata = new
            {
                barbeiros.TotalCount,
                barbeiros.PageSize,
                barbeiros.CurrentPage,
                barbeiros.TotalPages,
                barbeiros.HasNext,
                barbeiros.HasPrevious
            };



            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            var barbeirosDto = barbeiros.ToBarbeiroDTOList();

            return Ok(barbeirosDto);
        }






        [HttpGet("{id:int}", Name = "ObterBarbeiro")]
        public ActionResult<BarbeiroDTO> Get(int id)
        {

            var barbeiro = _uof.BarbeiroRepository.Get(b => b.BarbeiroId == id);
            if (barbeiro is null)
            {
                _logger.LogError($"barbeiro com id= {id} não encontrada...");
                return NotFound($"barbeiro com id= {id} não encontrado");
            }

            var barbeirosDto = barbeiro.ToBarbeiroDTO();

            return Ok(barbeirosDto);




        }




        [HttpPost]
        public ActionResult<BarbeiroDTO> Post(BarbeiroDTO barbeiroDto)
        {
            if (barbeiroDto is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            var barbeiro = barbeiroDto.ToBarbeiro();
            var barbeiroCriado = _uof.BarbeiroRepository.Create(barbeiro);
            _uof.Commit();

            var novoBarbeiroDto = barbeiroCriado.ToBarbeiroDTO();

            return new CreatedAtRouteResult("ObterBarbeiro",
            new { id = novoBarbeiroDto.BarbeiroId }, novoBarbeiroDto);

        }





        [HttpPut("{id:int}")]
        public ActionResult<BarbeiroDTO> Put(int id, BarbeiroDTO barbeiroDto)
        {
            if (id != barbeiroDto.BarbeiroId)
            {
                return BadRequest("Não encontrado");
            }


            var barbeiro = barbeiroDto.ToBarbeiro();
            var barbeiroAtualizado = _uof.BarbeiroRepository.Update(barbeiro);
            _uof.Commit();

            var barbeiroAtualizadoDto = barbeiroAtualizado.ToBarbeiroDTO();

            return Ok(barbeiroAtualizadoDto);

        }





        [HttpDelete("{id:int}")]
        public ActionResult<BarbeiroDTO> Delete(int id)
        {
            var barbeiro = _uof.BarbeiroRepository.Get(x => x.BarbeiroId == id);
            if (barbeiro is null)
            {
                return NotFound($"barbeiro com id= {id} não Localizada...");

            }


            var barbeiroExcluido = _uof.BarbeiroRepository.Delete(barbeiro);
            _uof.Commit();


            var barbeiroExcluidoDto = barbeiroExcluido.ToBarbeiroDTO();

            return Ok(barbeiroExcluidoDto);

        }














    }
}