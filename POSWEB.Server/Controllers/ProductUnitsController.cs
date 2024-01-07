using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POSWEB.Server.Context;
using POSWEB.Server.Entitites;

namespace POSWEB.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductUnitsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductUnitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductUnits
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductUnit>>> GetProductUnits()
        {
            return await _context.ProductUnits.ToListAsync();
        }

        // GET: api/ProductUnits/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductUnit>> GetProductUnit(short id)
        {
            var productUnit = await _context.ProductUnits.FindAsync(id);

            if (productUnit == null)
            {
                return NotFound();
            }

            return productUnit;
        }

        // PUT: api/ProductUnits/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductUnit(short id, ProductUnit productUnit)
        {
            if (id != productUnit.Id)
            {
                return BadRequest();
            }

            _context.Entry(productUnit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductUnitExists(id))
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

        // POST: api/ProductUnits
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductUnit>> PostProductUnit(ProductUnitPayload productUnit)
        {
            var entity = new ProductUnit
            {
                Description = productUnit.Description,
                CreatedById = productUnit.CreatedById,
                Id = 0,
                IsActive = productUnit.IsActive,
                UnitName = productUnit.UnitName,
                CreatedTime = productUnit.CreatedTime,
                
            };
            _context.ProductUnits.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductUnit", new { id = entity.Id }, productUnit);
        }

        // DELETE: api/ProductUnits/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductUnit(short id)
        {
            var productUnit = await _context.ProductUnits.FindAsync(id);
            if (productUnit == null)
            {
                return NotFound();
            }

            _context.ProductUnits.Remove(productUnit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductUnitExists(short id)
        {
            return _context.ProductUnits.Any(e => e.Id == id);
        }
    }
}
