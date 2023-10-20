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
    public class FaciltyController : ControllerBase
    {
        private readonly EzRentalDbContext _context;

        public FaciltyController(EzRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/Facilty
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facilties>>> GetFacilities()
        {
          if (_context.Facilities == null)
          {
              return NotFound();
          }
            return await _context.Facilities.ToListAsync();
        }

        // GET: api/Facilty/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Facilties>> GetFacilties(int id)
        {
          if (_context.Facilities == null)
          {
              return NotFound();
          }
            var facilties = await _context.Facilities.FindAsync(id);

            if (facilties == null)
            {
                return NotFound();
            }

            return facilties;
        }

        // PUT: api/Facilty/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFacilties(int id, Facilties facilties)
        {
            if (id != facilties.FacilityId)
            {
                return BadRequest();
            }

            _context.Entry(facilties).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaciltiesExists(id))
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

        // POST: api/Facilty
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Facilties>> PostFacilties(Facilties facilties)
        {
          if (_context.Facilities == null)
          {
              return Problem("Entity set 'EzRentalDbContext.Facilities'  is null.");
          }
            _context.Facilities.Add(facilties);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFacilties", new { id = facilties.FacilityId }, facilties);
        }

        // DELETE: api/Facilty/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacilties(int id)
        {
            if (_context.Facilities == null)
            {
                return NotFound();
            }
            var facilties = await _context.Facilities.FindAsync(id);
            if (facilties == null)
            {
                return NotFound();
            }

            _context.Facilities.Remove(facilties);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FaciltiesExists(int id)
        {
            return (_context.Facilities?.Any(e => e.FacilityId == id)).GetValueOrDefault();
        }
    }
}
