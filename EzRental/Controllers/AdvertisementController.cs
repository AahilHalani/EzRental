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
        public async Task<ActionResult<IEnumerable<Advertisement>>> GetAllAdvertisement()
        {
            if(_context.Advertisement == null)
            {
                return Problem("Server ran into an unexpected error");
            }

            var advertisements = await _context.Advertisement.Include(ad => ad.Area).ToListAsync();
                //Include(ad => ad.Area.city).Include(ad => ad.Area.city.Country).ToListAsync();

            if (advertisements.Count > 0) { return Ok(advertisements); } else return NotFound(); 
        }

        // GET: api/Advertisement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Advertisement>> GetAdvertisement(int id)
        {
          if (_context.Advertisement == null)
          {
              return NotFound();
          }

            var advertisement = await _context.Advertisement.Include(ad => ad.Rent).Include(ad => ad.Rent.Room).
                Include(ad => ad.Rent.Renter).Include(ad => ad.Area).
                  Include(ad => ad.Area.city).Include(ad => ad.Area.city.Country).FirstAsync(ad => ad.AdId == id);

            if(advertisement == null)
            {
                return NotFound();
            }

            AdvertisementWrapper advertisementWrapper = new AdvertisementWrapper();
            advertisementWrapper.advertisement = advertisement;
            advertisementWrapper.facilties = new List<Facilties>();

            var facilities = _context.AdFacility.Include(af => af.Facility).Where(af => af.AdId == id).ToList();

            if(facilities != null)
                foreach(var facility in facilities)
                {   
                    if(facility.Facility != null)
                        advertisementWrapper.facilties.Add(facility.Facility);
                }

            return Ok(advertisementWrapper);
        }

        // PUT: api/Advertisement/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvertisement(int id, AdvertisementWrapper advertisementWrapper)
        {
            try
            {
                Advertisement advertisement = advertisementWrapper.advertisement;
                List<Facilties> facilities = advertisementWrapper.facilties;


                if (id != advertisement.AdId)
                {
                    return BadRequest();
                }

                _context.Entry(advertisement.Rent.Room).State = EntityState.Modified;
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

                var existing_facilities = _context.AdFacility.Where(ad => ad.AdId == id).Select(ad => ad.FacilityId).ToListAsync();

                List<int> facility_ids = new List<int>();
                foreach(var facility in facilities)
                {
                    facility_ids.Add(facility.FacilityId);
                }
                
                // add new facilities
                foreach(var facility in facilities) 
                {
                    if (!existing_facilities.Result.Contains(facility.FacilityId))
                    {
                        Console.WriteLine("New Facility");
                        _context.AdFacility.Add(new AdFacility { FacilityId = facility.FacilityId, AdId = id });
                    }
                    else
                    {
                        Console.WriteLine($"Same facility {facility.FacilityId} detected");
                    }
                }

                // remove facilities
                foreach (var facility in existing_facilities.Result)
                {
                    if (!facility_ids.Contains(facility))
                    {
                        Console.WriteLine($"Remove Facility {facility}");
                        AdFacility adfacility = await _context.AdFacility.FirstOrDefaultAsync(af => af.FacilityId == facility && af.AdId == id);
                        _context.AdFacility.Remove(adfacility);
                    }
                }


                await _context.SaveChangesAsync();


                return NoContent();
            }
            catch(Exception e) 
            {
                Console.WriteLine(e.ToString());
                return Problem("Server ran into an error");
            }
        }

        // POST: /advertisement
        [HttpPost]
        public async Task<ActionResult<Advertisement>> PostAdvertisement(AdvertisementWrapper advertisementWrapper)
        {
            if (_context.Advertisement == null)
            {
                return Problem("Server ran into an unexpected error.");
            }

            try
            {
                if (advertisementWrapper.advertisement == null || advertisementWrapper.facilties == null)
                    return Problem("Server ran into an error: Null Arguments are passed");

                Advertisement advertisement = advertisementWrapper.advertisement;
                List<Facilties> facilities = advertisementWrapper.facilties;

                _context.Room.Add(advertisement.Rent.Room);
                _context.SaveChanges();

                advertisement.Rent.RoomId = advertisement.Rent.Room.RoomId;
                advertisement.Rent.Renter = null;
                _context.Rent.Add(advertisement.Rent);
                _context.SaveChanges();
                
                advertisement.RentId = advertisement.Rent.RentId;
                advertisement.Rent = null;
                advertisement.Area = null;
                _context.Advertisement.Add(advertisement);
                _context.SaveChanges();

                foreach(var facility in facilities)
                {
                    _context.AdFacility.Add(new AdFacility { FacilityId = facility.FacilityId,AdId=advertisement.AdId });
                }

                await _context.SaveChangesAsync();
                return CreatedAtAction("GetAdvertisement", new { id = advertisement.AdId });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return Problem("Server ran into an unexpected error.");
            }
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
