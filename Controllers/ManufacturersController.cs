using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KIP_1_API.Models;

namespace KIP_1_API.Controllers
{
    [Route("api/manufacturers")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ManufacturersController(MyDbContext context)
        {
            _context = context;

            if (_context.Manufacturers.Count() == 0)
            {
                _context.Manufacturers.Add(new Manufacturers
                {
                    ManufId = 1,
                    Name = "Toyota"
                });

                _context.Manufacturers.Add(new Manufacturers
                {
                    ManufId = 2,
                    Name = "Lada"
                });

                _context.Manufacturers.Add(new Manufacturers
                {
                    ManufId = 3,
                    Name = "Audi"
                });

                _context.SaveChanges(); //сохраняем добавленные в контекст данные
            }
        }

        // GET: api/Manufacturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturers>>> GetAllManufacturers()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        // GET: api/Manufacturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturers>> GetManufacturer([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var manufacturers = await _context.Manufacturers.FindAsync(id);

            if (manufacturers == null)
            {
                return NotFound();
            }

            return Ok(manufacturers);
        }

        // PUT: api/Manufacturers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManufacturers([FromRoute] long id, [FromBody] Manufacturers manufacturers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = await _context.Manufacturers.FindAsync(id);
            if (manufacturers == null)
            {
                return NotFound();
            }
           
            _context.Manufacturers.Update(item);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturersExists(id))
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

        // POST: api/Manufacturers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Manufacturers>> PostManufacturers([FromBody] Manufacturers manufacturers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Manufacturers.Add(manufacturers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetManufacturer", new { id = manufacturers.ManufId }, manufacturers);
        }

        // DELETE: api/Manufacturers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManufacturers([FromRoute]long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var manufacturers = await _context.Manufacturers.FindAsync(id);
            if (manufacturers == null)
            {
                return NotFound();
            }

            _context.Manufacturers.Remove(manufacturers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManufacturersExists(long id)
        {
            return _context.Manufacturers.Any(e => e.ManufId == id);
        }
    }
}
