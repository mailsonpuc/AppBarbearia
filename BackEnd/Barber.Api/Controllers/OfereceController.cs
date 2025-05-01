
using Barber.Api.DTOS;
using Barber.Api.DTOS.Mappings;
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

        private readonly IUnitOfWork _uof;

        private readonly ILogger<OfereceController> _logger;

        public OfereceController(IUnitOfWork uof,
        ILogger<OfereceController> logger)
        {
            _uof = uof;
            _logger = logger;
        }





        [HttpGet]
        public ActionResult<IEnumerable<OfereceDTO>> Get()
        {

            var ofereces = _uof.OfereceRepository.GetAll();
            if (ofereces is null)
            {
                return NotFound("ofereces Não encontrado");
            }

            var oferecesDto = ofereces.ToOfereceDTOList();

            return Ok(oferecesDto);



        }





        [HttpGet("{id:int}", Name = "ObterOferece")]
        public ActionResult<OfereceDTO> Get(int id)
        {

            var oferece = _uof.OfereceRepository.Get(o => o.ServicoId == id);
            if (oferece is null)
            {
                _logger.LogError($"oferece com id= {id} não encontrada...");
                return NotFound($"oferece com id= {id} não encontrado");
            }

            var ofereceDto = oferece.ToOfereceDTO();
            return Ok(ofereceDto);


        }




        [HttpPost]
        public ActionResult<OfereceDTO> Post(OfereceDTO ofereceDto)
        {
            if (ofereceDto is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            var oferece = ofereceDto.ToOferece();
            var ofereceCriado = _uof.OfereceRepository.Create(oferece);
            _uof.Commit();

            var novoOfereceDto = ofereceCriado.ToOfereceDTO();

            return new CreatedAtRouteResult("ObterOferece",
            new { id = novoOfereceDto.ServicoId }, novoOfereceDto);

        }





        [HttpPut("{id:int}")]
        public ActionResult<OfereceDTO> Put(int id, OfereceDTO ofereceDto)
        {
            if (id != ofereceDto.ServicoId)
            {
                return BadRequest("Não encontrado");
            }

            var oferece = ofereceDto.ToOferece();

            var ofereceAtualizado = _uof.OfereceRepository.Update(oferece);
            _uof.Commit();

            //return NoContent();
            return Ok(ofereceAtualizado);

        }





        [HttpDelete("{id:int}")]
        public ActionResult<OfereceDTO> Delete(int id)
        {
            var oferece = _uof.OfereceRepository.Get(o => o.ServicoId == id);
            if (oferece is null)
            {
                return NotFound($"oferece com id= {id} não Localizada...");

            }




            var ofereceExcluido = _uof.OfereceRepository.Delete(oferece);

            return Ok(ofereceExcluido);

        }





    }
}