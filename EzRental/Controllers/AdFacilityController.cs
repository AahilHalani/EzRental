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
    public class AdFacilityController : ControllerBase
    {
        private readonly EzRentalDbContext _context;

        public AdFacilityController(EzRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/AdFacility
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdFacility>>> GetAdFacility()
        {
          if (_context.AdFacility == null)
          {
              return NotFound();
          }
            return await _context.AdFacility.ToListAsync();
        }

        // GET: api/AdFacility/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdFacility>> GetAdFacility(int id)
        {
          if (_context.AdFacility == null)
          {
              return NotFound();
          }
            var adFacility = await _context.AdFacility.FindAsync(id);

            if (adFacility == null)
            {
                return NotFound();
            }

            return adFacility;
        }

        // PUT: api/AdFacility/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdFacility(int id, AdFacility adFacility)
        {
            if (id != adFacility.AdFacilityId)
            {
                return BadRequest();
            }

            _context.Entry(adFacility).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdFacilityExists(id))
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

        // POST: api/AdFacility
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdFacility>> PostAdFacility(AdFacility adFacility)
        {
          if (_context.AdFacility == null)
          {
              return Problem("Entity set 'EzRentalDbContext.AdFacility'  is null.");
          }
            _context.AdFacility.Add(adFacility);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdFacility", new { id = adFacility.AdFacilityId }, adFacility);
        }

        // DELETE: api/AdFacility/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdFacility(int id)
        {
            if (_context.AdFacility == null)
            {
                return NotFound();
            }
            var adFacility = await _context.AdFacility.FindAsync(id);
            if (adFacility == null)
            {
                return NotFound();
            }

            _context.AdFacility.Remove(adFacility);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdFacilityExists(int id)
        {
            return (_context.AdFacility?.Any(e => e.AdFacilityId == id)).GetValueOrDefault();
        }
    }
}
