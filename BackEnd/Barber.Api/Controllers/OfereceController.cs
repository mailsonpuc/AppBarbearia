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
    public class OfereceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OfereceController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Oferece
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Oferece>>> GetOferece()
        {
            return await _context.Oferece.AsNoTracking().ToListAsync();
        }

        // GET: api/Oferece/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Oferece>> GetOferece(int id)
        {
            var oferece = await _context.Oferece.FindAsync(id);

            if (oferece == null)
            {
                return NotFound();
            }

            return oferece;
        }

        // PUT: api/Oferece/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOferece(int id, Oferece oferece)
        {
            if (id != oferece.IdBarbeiro)
            {
                return BadRequest();
            }

            _context.Entry(oferece).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfereceExists(id))
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

        // POST: api/Oferece
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Oferece>> PostOferece(Oferece oferece)
        {
            _context.Oferece.Add(oferece);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OfereceExists(oferece.IdBarbeiro))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOferece", new { id = oferece.IdBarbeiro }, oferece);
        }

        // DELETE: api/Oferece/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOferece(int id)
        {
            var oferece = await _context.Oferece.FindAsync(id);
            if (oferece == null)
            {
                return NotFound();
            }

            _context.Oferece.Remove(oferece);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OfereceExists(int id)
        {
            return _context.Oferece.Any(e => e.IdBarbeiro == id);
        }
    }
}
