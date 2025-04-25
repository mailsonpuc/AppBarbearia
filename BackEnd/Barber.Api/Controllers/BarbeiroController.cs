using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Barber.Api.Context;
using Barber.Api.Models;

namespace Barber.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarbeiroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BarbeiroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Barbeiro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Barbeiro>>> GetBarbeiros()
        {
            return await _context.Barbeiros.AsTracking().ToListAsync();
        }

        // GET: api/Barbeiro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Barbeiro>> GetBarbeiro(int id)
        {
            var barbeiro = await _context.Barbeiros.FindAsync(id);

            if (barbeiro == null)
            {
                return NotFound();
            }

            return barbeiro;
        }

        // PUT: api/Barbeiro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBarbeiro(int id, Barbeiro barbeiro)
        {
            if (id != barbeiro.Id)
            {
                return BadRequest();
            }

            _context.Entry(barbeiro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarbeiroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Barbeiro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Barbeiro>> PostBarbeiro(Barbeiro barbeiro)
        {
            _context.Barbeiros.Add(barbeiro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBarbeiro", new { id = barbeiro.Id }, barbeiro);
        }

        // DELETE: api/Barbeiro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBarbeiro(int id)
        {
            var barbeiro = await _context.Barbeiros.FindAsync(id);
            if (barbeiro == null)
            {
                return NotFound();
            }

            _context.Barbeiros.Remove(barbeiro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BarbeiroExists(int id)
        {
            return _context.Barbeiros.Any(e => e.Id == id);
        }
    }
}
