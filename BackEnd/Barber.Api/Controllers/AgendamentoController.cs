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
    public class AgendamentoController : ControllerBase
    {
        private readonly AppDbContext _context;

        //construtor
        public AgendamentoController(AppDbContext context)
        {
            //injetendo a dependencia
            _context = context;
        }




        [HttpGet]
        public ActionResult<IEnumerable<Agendamento>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var agendamentos = _context.Agendamentos.AsNoTracking().ToList();
                if (agendamentos is null)
                {
                    return NotFound("agendamentos Não encontrado");
                }

                return agendamentos;

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpGet("{id:int}", Name = "ObterAgendamentos")]
        public ActionResult<Oferece> Get(int id)
        {
            try
            {
                var ag = _context.Agendamentos.AsNoTracking().FirstOrDefault(a => a.AgendamentoId == id);
                if (ag is null)
                {
                    return NotFound($"agendamento com id= {id} não encontrado");
                }

                return Ok(ag);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }




        [HttpPost]
        public ActionResult Post(Agendamento ag)
        {
            if (ag is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            _context.Agendamentos.Add(ag);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterAgendamentos",
            new { id = ag.AgendamentoId }, ag);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Agendamento ag)
        {
            if (id != ag.AgendamentoId)
            {
                return BadRequest("Não encontrado");
            }

            _context.Entry(ag).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            //return NoContent();
            return Ok(ag);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var ag = _context.Agendamentos.FirstOrDefault(c => c.AgendamentoId == id);
            if (ag is null)
            {
                return NotFound($"agendamento com id= {id} não Localizada...");

            }

            _context.Agendamentos.Remove(ag);
            _context.SaveChanges();


            return Ok($"agendamento com id= {id} removida");
        }



    }
}