
using Barber.Api.DTOS;
using Barber.Api.DTOS.Mappings;
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Barber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicoController : ControllerBase
    {
        //usando o repository

        private readonly IUnitOfWork _uof;

        private readonly ILogger<ServicoController> _logger;

        public ServicoController(IUnitOfWork uof,
        ILogger<ServicoController> logger)
        {
            _uof = uof;
            _logger = logger;
        }






        [HttpGet]
        public ActionResult<IEnumerable<ServicoDTO>> Get()
        {

            var servicos = _uof.ServicoRepository.GetAll();
            if (servicos is null)
            {
                return NotFound("servicos Não encontrado");
            }

            var servicosDto = servicos.ToServicoDTOList();

            return Ok(servicosDto);

        }






        [HttpGet("{id:int}", Name = "ObterServico")]
        public ActionResult<ServicoDTO> Get(int id)
        {

            var servico = _uof.ServicoRepository.Get(s => s.ServicoId == id);
            if (servico is null)
            {
                return NotFound($"servico com id= {id} não encontrado");
            }

            var servicoDto = servico.ToServicoDTO();

            return Ok(servicoDto);

        }





        [HttpPost]
        public ActionResult<ServicoDTO> Post(ServicoDTO servicoDto)
        {
            if (servicoDto is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }


            var servico = servicoDto.ToServico();
            var servicoCriado = _uof.ServicoRepository.Create(servico);
            _uof.Commit();

            var novoServicoDto = servicoCriado.ToServicoDTO();

            return new CreatedAtRouteResult("ObterServico",
            new { id = novoServicoDto.ServicoId }, novoServicoDto);

        }





        [HttpPut("{id:int}")]
        public ActionResult<ServicoDTO> Put(int id, ServicoDTO servicoDto)
        {
            if (id != servicoDto.ServicoId)
            {
                return BadRequest("Não encontrado");
            }

            var servico = servicoDto.ToServico();

            var servicoAtualizado = _uof.ServicoRepository.Update(servico);

            _uof.Commit();



            var servicoAtualizadoDto = servicoAtualizado.ToServicoDTO();

            return Ok(servicoAtualizadoDto);

        }




        [HttpDelete("{id:int}")]
        public ActionResult<ServicoDTO> Delete(int id)
        {
            var servico = _uof.ServicoRepository.Get(c => c.ServicoId == id);
            if (servico is null)
            {
                return NotFound($"servico com id= {id} não Localizada...");

            }


            var servicoExcluida = _uof.ServicoRepository.Delete(servico);
            _uof.Commit();

            //converte Servico para ServicoDTO

            var servicoExcluidaDto = servicoExcluida.ToServicoDTO();

            return Ok(servicoExcluidaDto);


        }





    }
}