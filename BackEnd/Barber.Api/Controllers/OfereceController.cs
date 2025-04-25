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
    public class OfereceController : ControllerBase
    {
        private readonly AppDbContext _context;

        //construtor
        public OfereceController(AppDbContext context)
        {
            //injetendo a dependencia
            _context = context;
        }




        [HttpGet]
        public ActionResult<IEnumerable<Oferece>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var servicos = _context.Oferece.AsNoTracking().ToList();
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





        [HttpGet("{id:int}", Name = "ObterOferece")]
        public ActionResult<Oferece> Get(int id)
        {
            try
            {
                var oferece = _context.Oferece.AsNoTracking().FirstOrDefault(o => o.ServicoId == id);
                if (oferece is null)
                {
                    return NotFound($"oferece com id= {id} não encontrado");
                }

                return Ok(oferece);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }




        [HttpPost]
        public ActionResult Post(Oferece oferece)
        {
            if (oferece is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            _context.Oferece.Add(oferece);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterOferece",
            new { id = oferece.ServicoId }, oferece);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Oferece oferece)
        {
            if (id != oferece.ServicoId)
            {
                return BadRequest("Não encontrado");
            }

            _context.Entry(oferece).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            //return NoContent();
            return Ok(oferece);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var oferece = _context.Oferece.FirstOrDefault(c => c.ServicoId == id);
            if (oferece is null)
            {
                return NotFound($"oferece com id= {id} não Localizada...");

            }

            _context.Oferece.Remove(oferece);
            _context.SaveChanges();


            return Ok($"oferece com id= {id} removida");
        }





    }
}