
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
        private readonly IRepository<Cliente> _repository;

        //construtor
        public ClienteController(IRepository<Cliente> repository)
        {
            //injetendo a dependencia
            _repository = repository;
        }





        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var clientes = _repository.GetAll();
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
                var cliente = _repository.Get(c => c.ClienteId == id);
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

            var clienteCriado = _repository.Create(cliente);

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

            _repository.Update(cliente);
            return Ok(cliente);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var cliente = _repository.Get(c => c.ClienteId == id);
            if (cliente is null)
            {
                return NotFound($"cliente com id= {id} não Localizada...");

            }



            var clienteExcluido = _repository.Delete(cliente);
            return Ok(clienteExcluido);

        }








    }
}