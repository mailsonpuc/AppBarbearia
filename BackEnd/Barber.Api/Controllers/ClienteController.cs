
using Barber.Api.Models;
using Barber.Api.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Barber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        //usando o repository
        private readonly IUnitOfWork _uof;

        public ClienteController(IUnitOfWork uof)
        {
            _uof = uof;
        }





        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var clientes = _uof.ClienteRepository.GetAll();
                if (clientes is null)
                {
                    return NotFound("clientes Não encontrado");
                }

                return Ok(clientes);

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }








        [HttpGet("{id:int}", Name = "ObterCliente")]
        public ActionResult<Cliente> Get(int id)
        {
            try
            {
                var cliente = _uof.ClienteRepository.Get(c => c.ClienteId == id);
                if (cliente is null)
                {
                    return NotFound($"cliente com id= {id} não encontrado");
                }

                return Ok(cliente);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }




        [HttpPost]
        public ActionResult Post(Cliente cliente)
        {
            if (cliente is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            var clienteCriado = _uof.ClienteRepository.Create(cliente);
            _uof.Commit();

            return new CreatedAtRouteResult("ObterCliente",
            new { id = clienteCriado.ClienteId }, clienteCriado);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest("Não encontrado");
            }

            _uof.ClienteRepository.Update(cliente);
            _uof.Commit();
            return Ok(cliente);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var cliente = _uof.ClienteRepository.Get(c => c.ClienteId == id);
            if (cliente is null)
            {
                return NotFound($"cliente com id= {id} não Localizada...");

            }



            var clienteExcluido = _uof.ClienteRepository.Delete(cliente);
            _uof.Commit();
            return Ok(clienteExcluido);

        }








    }
}