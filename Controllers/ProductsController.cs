using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly OvoshebazaContext _context;

        public ProductsController(OvoshebazaContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            // var t = _context.Entry(product);
            //t.State = EntityState.Modified;
            var existingCart = _context.Products.Find(product.Id);
            if (existingCart != null)
            {
                
                var attachedEntry = _context.Entry(existingCart);
                attachedEntry.CurrentValues.SetValues(product);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            
            /*var attachedEntry = _context.Entry(product);
            _context.Products.Remove(product);
            attachedEntry.State = EntityState.Deleted;*/

            //var product1 = new Product { Id = 1 };
            _context.Products.Attach(product);
            if (product.DeliveryCompositions.Count == 0 && product.PurchaseCompositions.Count == 0 && product.ShipmentCompositions.Count == 0)
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
            }
           
            //_context.SaveChanges();



            
            /*product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                return Conflict();
            }*/
            

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
