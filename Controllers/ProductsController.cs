using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using System.Reflection.Metadata;
using Microsoft.Extensions.Primitives;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
           ActionResult res =  CheckUserCredentials(new List<string>() { "admin", "client" });
           if(res is  Microsoft.AspNetCore.Mvc.OkResult)
            {
                return await _context.Products.ToListAsync();
            }
            else
            {
                return res;
            }
                
        }

        [HttpGet("GetCheckProblem")]
        public ActionResult CheckProblem()
        {
            return Problem("Текст");
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


            var purchaseCompositions = _context.PurchaseCompositions.Where(p => p.Idproduct == id);
            var deliveryCompositions = _context.DeliveryCompositions.Where(p => p.Idproduct == id);
            var shipmentCompositions = _context.ShipmentCompositions.Where(p => p.Idproduct == id);
            //var attachedEntry = _context.Entry(product);
            //product = attachedEntry.Entity;
            //object currentName2 = context.Entry(blog).Property("Name").CurrentValue;
            /*_context.Products.Remove(product);
            attachedEntry.State = EntityState.Deleted;*/

            //var product1 = new Product { Id = 1 };
            _context.Products.Attach(product);
            if (deliveryCompositions.Count() == 0 && purchaseCompositions.Count() == 0 && shipmentCompositions.Count() == 0)
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
                return NoContent();
            }
           
            //_context.SaveChanges();



            
            /*product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                return Conflict();
            }*/
            

            return Conflict();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
        

        private ActionResult CheckUserCredentials(List<string> Roles)
        {
            StringValues pr = Request.Headers.Authorization;
            string dataAuth = pr[0].Substring(7);
            string[] mass = dataAuth.Split(' ');
            byte[] EncryptedData = new byte[mass.Length];
            for (int i = 0; i < mass.Length; i++)
            {
                EncryptedData[i] = Convert.ToByte(mass[i]);
            }
            //byte[] EncryptedData = Encoding.UTF8.GetBytes(ДанныеАвторизации);
            byte[] DecryptedData = _context.RsaKey.Decrypt(EncryptedData, false);
            string a = Encoding.UTF8.GetString(DecryptedData);
            string[] log = a.Split(' ');
            var Query  = _context.Users.Where(p => p.Login == log[0]);

            User user;

            if (Query.Count() == 0)
            {
                user = null;
                return Problem("Пользователь не найден");
            }
            else
            {
                if(Query.ToList()[0].Password != log[1])
                {
                    user = null;
                    return Problem("Пароли не совпадают");
                }
                else
                {
                    if(DateTime.Now.AddMinutes(-5) >= DateTime.Parse(log[2] + " " + log[3]))
                    {
                        user = null;
                        return Problem("Срок данных идентификации истек");

                    }
                    else
                    {
                        user = Query.ToList()[0];
                        if (Roles.Contains(user.UserRol))
                        {
                            return Ok();
                        }
                        else
                        {
                            return Problem("Отказ в доступе вашей роли");
                        }
                    }
                    
                }

                    
            }
            
        }

       


    }
    enum РезультатИдентификации
    {
        ИдентификацияУспешна, 
        СрокДанныхИдентификацииИстек,
        ПаролиНеСовпадают,
        ПользовательНеНайден
    }

}
