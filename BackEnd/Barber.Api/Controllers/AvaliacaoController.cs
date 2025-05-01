
using Barber.Api.DTOS;
using Barber.Api.DTOS.Mappings;
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
        public ActionResult<IEnumerable<AvaliacaoDTO>> Get()
        {

            var avaliacoes = _uof.AvaliacaoRepository.GetAll();
            if (avaliacoes is null)
            {
                return NotFound("avaliacoes Não encontrado");
            }

            var avaliacoesDto = avaliacoes.ToAvaliacaoDTOList();

            return Ok(avaliacoesDto);
        }





        [HttpGet("{id:int}", Name = "ObterAvaliacao")]
        public ActionResult<AvaliacaoDTO> Get(int id)
        {

            var avaliacao = _uof.AvaliacaoRepository.Get(a => a.AvaliacaoId == id);
            if (avaliacao is null)
            {
                _logger.LogWarning($"avaliação com id= {id} não encontrado...");
                return NotFound($"avaliação com id= {id} não encontrado");
            }

            var avaliacaoDto = avaliacao.ToAvaliacaoDTO();

            return Ok(avaliacaoDto);




        }




        [HttpPost]
        public ActionResult<AvaliacaoDTO> Post(AvaliacaoDTO avaliacaoDto)
        {
            if (avaliacaoDto is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            var avaliacao = avaliacaoDto.ToAvaliacao();
            var avaliacaoCriado = _uof.AvaliacaoRepository.Create(avaliacao);
            _uof.Commit();

            var novaAvaliacaoDto = avaliacaoCriado.ToAvaliacaoDTO();

            return new CreatedAtRouteResult("ObterAvaliacao",
            new { id = novaAvaliacaoDto.AvaliacaoId }, novaAvaliacaoDto);

        }





        [HttpPut("{id:int}")]
        public ActionResult<AvaliacaoDTO> Put(int id, AvaliacaoDTO avaliacaoDto)
        {
            if (id != avaliacaoDto.AvaliacaoId)
            {
                return BadRequest("Não encontrado");
            }


            var avaliacao = avaliacaoDto.ToAvaliacao();
            var avaliacaoAtualizado = _uof.AvaliacaoRepository.Update(avaliacao);
            _uof.Commit();



            var avaliacaoAtualizadoDto = avaliacaoAtualizado.ToAvaliacaoDTO();

            return Ok(avaliacaoAtualizadoDto);
        }





        [HttpDelete("{id:int}")]
        public ActionResult<AvaliacaoDTO> Delete(int id)
        {
            var avaliacao = _uof.AvaliacaoRepository.Get(a => a.AvaliacaoId == id);
            if (avaliacao is null)
            {
                return NotFound($"avaliação com id= {id} não Localizada...");

            }



            var avaliacaoExcluido = _uof.AvaliacaoRepository.Delete(avaliacao);
            _uof.Commit();


            var avaliacaoExcluidaDto = avaliacaoExcluido.ToAvaliacaoDTO();

            return Ok(avaliacaoExcluidaDto);

        }





    }
}