using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Barber.Api.Context;
using Barber.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barber.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly AppDbContext _context;

        //construtor
        public ClienteController(AppDbContext context)
        {
            //injetendo a dependencia
            _context = context;
        }





        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var clientes = _context.Clientes.AsNoTracking().ToList();
                if (clientes is null)
                {
                    return NotFound("clientes Não encontrado");
                }

                return clientes;

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
                var cliente = _context.Clientes.AsNoTracking().FirstOrDefault(cl => cl.ClienteId == id);
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

            _context.Clientes.Add(cliente);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCliente",
            new { id = cliente.ClienteId }, cliente);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest("Não encontrado");
            }

            _context.Entry(cliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            //return NoContent();
            return Ok(cliente);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var cliente = _context.Clientes.FirstOrDefault(c => c.ClienteId == id);
            if (cliente is null)
            {
                return NotFound($"cliente com id= {id} não Localizada...");

            }

            _context.Clientes.Remove(cliente);
            _context.SaveChanges();


            return Ok($"cliente com id= {id} removida");
        }








    }
}