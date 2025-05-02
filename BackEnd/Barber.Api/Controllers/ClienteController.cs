
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
    public class ClienteController : ControllerBase
    {
        //usando o repository
        private readonly IUnitOfWork _uof;

        private readonly ILogger<ClienteController> _logger;

        public ClienteController(IUnitOfWork uof,
        ILogger<ClienteController> logger)
        {
            _uof = uof;
            _logger = logger;
        }





        [HttpGet]
        public ActionResult<IEnumerable<ClienteDTO>> Get()
        {

            var clientes = _uof.ClienteRepository.GetAll();
            if (clientes is null)
            {
                return NotFound("clientes Não encontrado");
            }

            var clientesDto = clientes.ToClienteDTOList();

            return Ok(clientesDto);




        }






        [HttpGet("filter/nome/pagination")]
        public ActionResult<IEnumerable<ClienteDTO>> GetClientesFiltrados(
                                       [FromQuery] ClienteFiltroNome clientesFiltro)
        {
            var clientesFiltrados = _uof.ClienteRepository
                                         .GetClientesFiltroNome(clientesFiltro);

            return ObterClientes(clientesFiltrados);

        }

        private ActionResult<IEnumerable<ClienteDTO>> ObterClientes(PagedList<Cliente> clientes)
        {
            var metadata = new
            {
                clientes.TotalCount,
                clientes.PageSize,
                clientes.CurrentPage,
                clientes.TotalPages,
                clientes.HasNext,
                clientes.HasPrevious
            };

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            var clientesDto = clientes.ToClienteDTOList();
            return Ok(clientesDto);
        }







        [HttpGet("{id:int}", Name = "ObterCliente")]
        public ActionResult<ClienteDTO> Get(int id)
        {

            var cliente = _uof.ClienteRepository.Get(c => c.ClienteId == id);
            if (cliente is null)
            {
                _logger.LogError($"cliente com id= {id} não encontrada...");
                return NotFound($"cliente com id= {id} não encontrado");
            }

            var clienteDto = cliente.ToClienteDTO();

            return Ok(clienteDto);


        }




        [HttpPost]
        public ActionResult<ClienteDTO> Post(ClienteDTO clienteDto)
        {
            if (clienteDto is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            var cliente = clienteDto.ToCliente();

            var clienteCriado = _uof.ClienteRepository.Create(cliente);
            _uof.Commit();

            var novoClienteDto = clienteCriado.ToClienteDTO();

            return new CreatedAtRouteResult("ObterCliente",
            new { id = novoClienteDto.ClienteId }, novoClienteDto);

        }





        [HttpPut("{id:int}")]
        public ActionResult<ClienteDTO> Put(int id, ClienteDTO clienteDto)
        {
            if (id != clienteDto.ClienteId)
            {
                return BadRequest("Não encontrado");
            }

            var cliente = clienteDto.ToCliente();
            var clienteAtualizado = _uof.ClienteRepository.Update(cliente);
            _uof.Commit();




            var clienteAtualizadoDto = clienteAtualizado.ToClienteDTO();

            return Ok(clienteAtualizadoDto);

        }





        [HttpDelete("{id:int}")]
        public ActionResult<ClienteDTO> Delete(int id)
        {
            var cliente = _uof.ClienteRepository.Get(c => c.ClienteId == id);
            if (cliente is null)
            {
                return NotFound($"cliente com id= {id} não Localizada...");

            }



            var clienteExcluido = _uof.ClienteRepository.Delete(cliente);
            _uof.Commit();


            var clienteExcluidoDto = clienteExcluido.ToClienteDTO();

            return Ok(clienteExcluidoDto);

        }








    }
}