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
    public class ServicoController : ControllerBase
    {
        private readonly AppDbContext _context;

        //construtor
        public ServicoController(AppDbContext context)
        {
            //injetendo a dependencia
            _context = context;
        }






        [HttpGet]
        public ActionResult<IEnumerable<Servico>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var servicos = _context.Servicos.AsNoTracking().ToList();
                if (servicos is null)
                {
                    return NotFound("servicos Não encontrado");
                }

                return servicos;

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }






        [HttpGet("{id:int}", Name = "ObterServico")]
        public ActionResult<Servico> Get(int id)
        {
            try
            {
                var servico = _context.Servicos.AsNoTracking().FirstOrDefault(s => s.ServicoId == id);
                if (servico is null)
                {
                    return NotFound($"servico com id= {id} não encontrado");
                }

                return Ok(servico);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpPost]
        public ActionResult Post(Servico servico)
        {
            if (servico is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            _context.Servicos.Add(servico);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterServico",
            new { id = servico.ServicoId }, servico);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Servico servico)
        {
            if (id != servico.ServicoId)
            {
                return BadRequest("Não encontrado");
            }

            _context.Entry(servico).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            //return NoContent();
            return Ok(servico);

        }




        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var servico = _context.Servicos.FirstOrDefault(c => c.ServicoId == id);
            if (servico is null)
            {
                return NotFound($"servico com id= {id} não Localizada...");

            }

            _context.Servicos.Remove(servico);
            _context.SaveChanges();


            return Ok($"servico com id= {id} removida");
        }





    }
}