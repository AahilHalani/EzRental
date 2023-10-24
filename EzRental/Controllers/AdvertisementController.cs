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
    public class AdvertisementController : ControllerBase
    {
        private readonly EzRentalDbContext _context;

        public AdvertisementController(EzRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/Advertisement
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Advertisement>>> GetAdvertisement()
        {
          if (_context.Advertisement == null)
          {
              return NotFound();
          }
            return await _context.Advertisement.Include(a => a.Rent).Include(r => r.Rent.Room).ToListAsync();
        }

        // GET: api/Advertisement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Advertisement>> GetAdvertisement(int id)
        {
          if (_context.Advertisement == null)
          {
              return NotFound();
          }
          
          var advertisement = await _context.Advertisement.Include(a => a.Rent).Include(r => r.Rent.Room).FirstAsync(a => a.AdId == id);

          if (advertisement == null)
          {
            return NotFound();
          }

          return Ok(advertisement);
        }

        // PUT: api/Advertisement/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvertisement(int id, Advertisement advertisement)
        {
            if (id != advertisement.AdId)
            {
                return BadRequest();
            }

            _context.Entry(advertisement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertisementExists(id))
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

        // POST: api/Advertisement
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Advertisement>> PostAdvertisement(Advertisement advertisement)
        {
          if (_context.Advertisement == null)
          {
              return Problem("Entity set 'EzRentalDbContext.Advertisement'  is null.");
          }
            _context.Advertisement.Add(advertisement);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdvertisement", new { id = advertisement.AdId }, advertisement);
        }

        // DELETE: api/Advertisement/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvertisement(int id)
        {
            if (_context.Advertisement == null)
            {
                return NotFound();
            }

            // get advertisement data
            var advertisement = await _context.Advertisement.Include(a => a.Rent).Include(r => r.Rent.Room).FirstAsync(a => a.AdId == id);
            
            if (advertisement == null)
            {
                return NotFound();
            }

            //Get all facilites linked to add
            var facilities = await _context.AdFacility.Where(ad => ad.AdId == id).ToListAsync();
            
            //Remove all ad facilities
            foreach(var facility in facilities) 
            {   
                if(facility != null)
                    _context.AdFacility.Remove(facility);
            }

            _context.Advertisement.Remove(advertisement);
            _context.Rent.Remove(advertisement.Rent);
            _context.Room.Remove(advertisement.Rent.Room);

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool AdvertisementExists(int id)
        {
            return (_context.Advertisement?.Any(e => e.AdId == id)).GetValueOrDefault();
        }
    }
}
