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
    public class AvaliacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        //construtor
        public AvaliacaoController (AppDbContext context)
        {
            //injetendo a dependencia
            _context = context;
        }




        [HttpGet]
        public ActionResult<IEnumerable<Avaliacao>> Get()
        {
            try
            {
                //throw new Exception("ocorreu um erro");
                var avaliacoes = _context.Avaliacoes.AsNoTracking().ToList();
                if (avaliacoes is null)
                {
                    return NotFound("avaliacoes Não encontrado");
                }

                return avaliacoes;

            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }





        [HttpGet("{id:int}", Name = "ObterAvaliacao")]
        public ActionResult<Oferece> Get(int id)
        {
            try
            {
                var av = _context.Avaliacoes.AsNoTracking().FirstOrDefault(o => o.AvaliacaoId == id);
                if (av is null)
                {
                    return NotFound($"avaliação com id= {id} não encontrado");
                }

                return Ok(av);
            }

            catch (System.Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                "Ocorreu um problema ao tratar sua solicitação");
            }

        }




        [HttpPost]
        public ActionResult Post(Avaliacao av)
        {
            if (av is null)
            {
                return BadRequest("Ocorreu um erro 400");
            }

            _context.Avaliacoes.Add(av);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterAvaliacao",
            new { id = av.AvaliacaoId }, av);

        }





        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Avaliacao av)
        {
            if (id != av.AvaliacaoId)
            {
                return BadRequest("Não encontrado");
            }

            _context.Entry(av).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            //return NoContent();
            return Ok(av);

        }





        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var av = _context.Avaliacoes.FirstOrDefault(c => c.AvaliacaoId == id);
            if (av is null)
            {
                return NotFound($"avaliação com id= {id} não Localizada...");

            }

            _context.Avaliacoes.Remove(av);
            _context.SaveChanges();


            return Ok($"avaliação com id= {id} removida");
        }





    }
}