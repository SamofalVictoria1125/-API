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
    public class ContactPersonsController : ControllerBase
    {
        private readonly OvoshebazaContext _context;

        public ContactPersonsController(OvoshebazaContext context)
        {
            _context = context;
        }

        // GET: api/ContactPersons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactPerson>>> GetContactPeople()
        {
            return await _context.ContactPeople.ToListAsync();
        }

        // GET: api/ContactPersons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactPerson>> GetContactPerson(int id)
        {
            var contactPerson = await _context.ContactPeople.FindAsync(id);

            if (contactPerson == null)
            {
                return NotFound();
            }

            return contactPerson;
        }

        // PUT: api/ContactPersons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactPerson(int id, ContactPerson contactPerson)
        {
            if (id != contactPerson.Id)
            {
                return BadRequest();
            }

            var existingCart = _context.ContactPeople.Find(contactPerson.Id);
            if (existingCart != null)
            {

                var attachedEntry = _context.Entry(existingCart);
                attachedEntry.CurrentValues.SetValues(contactPerson);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactPersonExists(id))
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

        // POST: api/ContactPersons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactPerson>> PostContactPerson(ContactPerson contactPerson)
        {
            _context.ContactPeople.Add(contactPerson);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContactPerson", new { id = contactPerson.Id }, contactPerson);
        }

        // DELETE: api/ContactPersons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactPerson(int id)
        {
            var contactPerson = await _context.ContactPeople.FindAsync(id);
            if (contactPerson == null)
            {
                return NotFound();
            }


            var counterparty = _context.Counterparties.Where(p => p.IdContactPerson == id);
            var employee = _context.Employees.Where(p => p.IdcontactPerson == id);
 
            _context.ContactPeople.Attach(contactPerson);
            if (counterparty.Count() == 0 && employee.Count() == 0 )
            {
                _context.Remove(contactPerson);
                await _context.SaveChangesAsync();
                return NoContent();
            }

            return Conflict();
        }

        private bool ContactPersonExists(int id)
        {
            return _context.ContactPeople.Any(e => e.Id == id);
        }
    }
}
