using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EzRental.Data;
using EzRental.Models;

namespace EzRental.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly EzRentalDbContext _context;

        public AreaController(EzRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/Area
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Area>>> GetArea()
        {
          if (_context.Area == null)
          {
              return NotFound();
          }
            return await _context.Area.ToListAsync();
        }

        // GET: api/Area/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetArea(int id)
        {
          if (_context.Area == null)
          {
              return NotFound();
          }
            var area = await _context.Area.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }

            return area;
        }

        // PUT: api/Area/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(int id, Area area)
        {
            if (id != area.AreaId)
            {
                return BadRequest();
            }

            _context.Entry(area).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaExists(id))
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

        // POST: api/Area
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Area>> PostArea(Area area)
        {
          if (_context.Area == null)
          {
              return Problem("Entity set 'EzRentalDbContext.Area'  is null.");
          }
            _context.Area.Add(area);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArea", new { id = area.AreaId }, area);
        }

        // DELETE: api/Area/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            if (_context.Area == null)
            {
                return NotFound();
            }
            var area = await _context.Area.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }

            _context.Area.Remove(area);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AreaExists(int id)
        {
            return (_context.Area?.Any(e => e.AreaId == id)).GetValueOrDefault();
        }
    }
}
