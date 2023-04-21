////using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace КурсоваяAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyRatesController : ControllerBase
    {
        private readonly OvoshebazaContext _context;

        public CurrencyRatesController(OvoshebazaContext context)
        {
            _context = context;
        }

        // GET: api/CurrencyRates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyRate>>> GetCurrencyRate()
        {
            if (_context.CurrencyRate == null)
            {
                return NotFound();
            }
            return await _context.CurrencyRate.ToListAsync();
        }

        // GET: api/CurrencyRates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CurrencyRate>> GetCurrencyRate(int id)
        {
            if (_context.CurrencyRate == null)
            {
                return NotFound();
            }
            var currencyRate = await _context.CurrencyRate.FindAsync(id);

            if (currencyRate == null)
            {
                return NotFound();
            }

            return currencyRate;
        }

        // PUT: api/CurrencyRates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrencyRate(int id, CurrencyRate currencyRate)
        {
            if (id != currencyRate.Id)
            {
                return BadRequest();
            }

            _context.Entry(currencyRate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyRateExists(id))
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

        // POST: api/CurrencyRates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CurrencyRate>> PostCurrencyRate(CurrencyRate currencyRate)
        {
            if (_context.CurrencyRate == null)
            {
                return Problem("Entity set 'OvoshebazaContext.CurrencyRate'  is null.");
            }
            _context.CurrencyRate.Add(currencyRate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurrencyRate", new { id = currencyRate.Id }, currencyRate);
        }

        // DELETE: api/CurrencyRates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrencyRate(int id)
        {
            if (_context.CurrencyRate == null)
            {
                return NotFound();
            }
            var currencyRate = await _context.CurrencyRate.FindAsync(id);
            if (currencyRate == null)
            {
                return NotFound();
            }

            _context.CurrencyRate.Remove(currencyRate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurrencyRateExists(int id)
        {
            return (_context.CurrencyRate?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
