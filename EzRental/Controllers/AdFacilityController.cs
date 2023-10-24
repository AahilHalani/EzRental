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

        /*// GET: api/AdFacility
        [HttpGet]
        public ActionResult<IEnumerable<AdFacility>> GetAdFacility()
        {
          if (_context.AdFacility == null)
          {
              return NotFound();
          }
            return _context.AdFacility.Include(ad => ad.Facility).Include(ad => ad.Ad).ToList();
        }*/


        // GET: api/AdFacility/5
        // get all facilites belonging to the add id
        [HttpGet("{adId}")]
        public ActionResult<AdFacility> GetAdFacility(int adId)
        {
            Console.WriteLine(adId);
            if (_context.AdFacility == null)
            {
                return NotFound();
            }
            var adFacility = _context.AdFacility.Include(af => af.Facility).Where(af => af.AdId == adId).ToList();
            
            if (adFacility.Count <= 0)
            {
                return NotFound();
            }

            return Ok(adFacility);
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
