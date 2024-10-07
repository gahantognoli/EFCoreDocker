using EFCoreSqlServer.Context;
using EFCoreSqlServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreSqlServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() 
            => Ok(await _context.Products.ToListAsync());

        [HttpPost]
        public async Task<ActionResult<Product>> Create([FromBody] Product product)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(int id, [FromBody] Product productFromJson)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            product.Name = productFromJson.Name;
            product.Price = productFromJson.Price;
            product.Description = productFromJson.Description;

            await _context.SaveChangesAsync();

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }
    }
}
