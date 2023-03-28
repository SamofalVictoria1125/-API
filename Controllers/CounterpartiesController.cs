using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API;
using API.Models;

namespace КурсоваяAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterpartiesController : ControllerBase
    {
        private readonly OvoshebazaContext _context;

        public CounterpartiesController(OvoshebazaContext context)
        {
            _context = context;
        }

        // GET: api/Counterparties
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Counterparty>>> GetCounterparties()
        {
            return await _context.Counterparties.ToListAsync();
        }

        // GET: api/Counterparties/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Counterparty>> GetCounterparty(int id)
        {
            var counterparty = await _context.Counterparties.FindAsync(id);

            if (counterparty == null)
            {
                return NotFound();
            }

            return counterparty;
        }

        // PUT: api/Counterparties/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCounterparty(int id, Counterparty counterparty)
        {
            if (id != counterparty.Id)
            {
                return BadRequest();
            }

            _context.Entry(counterparty).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CounterpartyExists(id))
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

        // POST: api/Counterparties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Counterparty>> PostCounterparty(Counterparty counterparty)
        {
            _context.Counterparties.Add(counterparty);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCounterparty", new { id = counterparty.Id }, counterparty);
        }

        // DELETE: api/Counterparties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCounterparty(int id)
        {
            var counterparty = await _context.Counterparties.FindAsync(id);
            if (counterparty == null)
            {
                return NotFound();
            }

            _context.Counterparties.Remove(counterparty);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CounterpartyExists(int id)
        {
            return _context.Counterparties.Any(e => e.Id == id);
        }
    }
}
