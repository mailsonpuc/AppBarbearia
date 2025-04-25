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
    public class HistoricoCorteController : ControllerBase
    {
        private readonly AppDbContext _context;

        //construtor
        public HistoricoCorteController(AppDbContext context)
        {
            //injetendo a dependencia
            _context = context;
        }







        [HttpGet]
        public ActionResult<IEnumerable<HistoricoCorte>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var historicos = _context.HistoricosCorte.AsNoTracking().ToList();
                if (historicos is null)
                {
                    return NotFound("historicos Não encontrado");
                }

                return historicos;

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpGet("{id:int}", Name = "Obterhistorico")]
        public ActionResult<HistoricoCorte> Get(int id)
        {
            try
            {
                var historico = _context.HistoricosCorte.AsNoTracking().FirstOrDefault(hi => hi.HistoricoId == id);
                if (historico is null)
                {
                    return NotFound($"historico com id= {id} não encontrado");
                }

                return Ok(historico);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }




        [HttpPost]
        public ActionResult Post(HistoricoCorte historico)
        {
            if (historico is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            _context.HistoricosCorte.Add(historico);
            _context.SaveChanges();

            return new CreatedAtRouteResult("Obterhistorico",
            new { id = historico.HistoricoId }, historico);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, HistoricoCorte historico)
        {
            if (id != historico.HistoricoId)
            {
                return BadRequest("Não encontrado");
            }

            _context.Entry(historico).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            //return NoContent();
            return Ok(historico);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var historico = _context.HistoricosCorte.FirstOrDefault(h => h.HistoricoId == id);
            if (historico is null)
            {
                return NotFound($"historico com id= {id} não Localizada...");

            }

            _context.HistoricosCorte.Remove(historico);
            _context.SaveChanges();


            return Ok($"historico com id= {id} removida");
        }











    }
}