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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicKeyController : ControllerBase
    {
        private readonly OvoshebazaContext _context;

        public PublicKeyController(OvoshebazaContext context)
        {
            _context = context;

        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<string>> GetPublicKey()
        {

            return _context.publickey;

        }



    }
}
