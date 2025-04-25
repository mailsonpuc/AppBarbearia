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
    public class BarbeiroController : ControllerBase
    {
        private readonly AppDbContext _context;

        //construtor
        public BarbeiroController(AppDbContext context)
        {
            //injetendo a dependencia
            _context = context;
        }








        [HttpGet]
        public ActionResult<IEnumerable<Barbeiro>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var barbeiros = _context.Barbeiros.AsNoTracking().ToList();
                if (barbeiros is null)
                {
                    return NotFound("barbeiros Não encontrado");
                }

                return barbeiros;

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpGet("{id:int}", Name = "ObterBarbeiro")]
        public ActionResult<Barbeiro> Get(int id)
        {
            try
            {
                var barbeiro = _context.Barbeiros.AsNoTracking().FirstOrDefault(ba => ba.BarbeiroId == id);
                if (barbeiro is null)
                {
                    return NotFound($"barbeiro com id= {id} não encontrado");
                }

                return Ok(barbeiro);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }




        [HttpPost]
        public ActionResult Post(Barbeiro barbeiro)
        {
            if (barbeiro is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            _context.Barbeiros.Add(barbeiro);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterBarbeiro",
            new { id = barbeiro.BarbeiroId }, barbeiro);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Barbeiro barbeiro)
        {
            if (id != barbeiro.BarbeiroId)
            {
                return BadRequest("Não encontrado");
            }

            _context.Entry(barbeiro).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            //return NoContent();
            return Ok(barbeiro);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var barbeiro = _context.Barbeiros.FirstOrDefault(b => b.BarbeiroId == id);
            if (barbeiro is null)
            {
                return NotFound($"barbeiro com id= {id} não Localizada...");

            }

            _context.Barbeiros.Remove(barbeiro);
            _context.SaveChanges();


            return Ok($"barbeiro com id= {id} removida");
        }














    }
}