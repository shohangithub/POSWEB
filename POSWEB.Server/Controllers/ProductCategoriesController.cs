using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSWEB.Server.Context;
using POSWEB.Server.Entitites;

namespace POSWEB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductCategories

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(short id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);

            if (productCategory == null)
            {
                return NotFound();
            }

            return productCategory;
        }

        // PUT: api/ProductCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(short id, ProductCategory productCategory)
        {
            if (id != productCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(productCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
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

        // POST: api/ProductCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategoryPayload productCategory)
        {
            ProductCategory entity = new()
            {
                CategoryName = productCategory.CategoryName,
                CreatedById = 1,
                IsActive = true,
                Description = productCategory.Description,
                 CreatedTime = DateTime.UtcNow,
            };
            _context.ProductCategories.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductCategory", new { id = entity.Id }, productCategory);
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(short id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductCategoryExists(short id)
        {
            return _context.ProductCategories.Any(e => e.Id == id);
        }
    }
}
