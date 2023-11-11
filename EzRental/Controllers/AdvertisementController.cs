using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EzRental.Data;
using EzRental.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

            var advertisements = await _context.Advertisement.ToListAsync();
                //Include(ad => ad.Area.city).Include(ad => ad.Area.city.Country).ToListAsync();

            if (advertisements.Count > 0) { return Ok(advertisements); } else return NotFound(); 
        }

        // GET: api/Advertisement/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Advertisement>> GetAdvertisement(int id)
        {
            try
            {
                if (_context.Advertisement == null)
                {
                    return NotFound();
                }

                var advertisement = await _context.Advertisement.Include(ad => ad.Rent).Include(ad => ad.Rent.Room).
                    Include(ad => ad.Rent.Renter).FirstAsync(ad => ad.AdId == id);

                if (advertisement == null)
                {
                    return NotFound();
                }

                AdvertisementWrapper advertisementWrapper = new AdvertisementWrapper();
                advertisementWrapper.advertisement = advertisement;
                advertisementWrapper.facilties = new List<Facilties>();

                var facilities = _context.AdFacility.Include(af => af.Facility).Where(af => af.AdId == id).ToList();

                if (facilities != null)
                    foreach (var facility in facilities)
                    {
                        if (facility.Facility != null)
                            advertisementWrapper.facilties.Add(facility.Facility);
                    }

                return Ok(advertisementWrapper);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Advertisement/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvertisement(int id, AdvertisementWrapper advertisementWrapper)
        {
            if (_context == null)
                return BadRequest("Server ran into an error");

            if (advertisementWrapper.advertisement == null || advertisementWrapper.facilties == null)
                return BadRequest("Server ran into an error");

            if (!AdvertisementExists(id))
                return NotFound("Advertisement not Found");

            try
            {
                Advertisement advertisement = advertisementWrapper.advertisement;
                List<Facilties> facilities = advertisementWrapper.facilties;

                if (advertisement.Rent == null || advertisement.Rent.Room == null)
                    return BadRequest("Server ran into an error");

                //set advertisement Id
                advertisement.AdId = id;

                //set room id to be tracked
                int rent_id = await _context.Advertisement.Where(ad => ad.AdId == id).
                    Select(ad => ad.RentId).FirstAsync();
                int room_id = await _context.Rent.Where(r => r.RentId == rent_id).
                    Select(r => r.RoomId).FirstAsync();

                advertisement.Rent.Room.RoomId = room_id;
                advertisement.RentId = rent_id;

                
                _context.Entry(advertisement.Rent.Room).State = EntityState.Modified;
                _context.Entry(advertisement).State = EntityState.Modified;

                try
                {
                    _context.SaveChanges();
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

                var existing_facilities = _context.AdFacility.Where(ad => ad.AdId == id).
                    Select(ad => ad.FacilityId).ToListAsync();


                var facility_ids = facilities.Select(obj => obj.FacilityId).ToList();
                
                // add new facilities is the previous state of object does not already contains it
                foreach(var facility in facility_ids) 
                {
                    if (!existing_facilities.Result.Contains(facility))
                    {
                        Console.WriteLine("New Facility");
                        _context.AdFacility.Add(new AdFacility { FacilityId = facility, AdId = id });
                    }
                    else
                    {
                        Console.WriteLine($"Same facility {facility} detected");
                    }
                }

                // remove facilities is the current facilties object does not contains existing facilities
                foreach (var facility in existing_facilities.Result)
                {
                    if (!facility_ids.Contains(facility))
                    {
                        Console.WriteLine($"Remove Facility {facility}");
                        AdFacility adfacility = await _context.AdFacility.FirstOrDefaultAsync(af => af.FacilityId == facility && af.AdId == id);
                        
                        if (adfacility == null)
                            continue;

                        _context.AdFacility.Remove(adfacility);
                    }
                }

                await _context.SaveChangesAsync();


                return Ok();
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

                advertisement.Rent.Renter = null;
                _context.Advertisement.Add(advertisement);
                await _context.SaveChangesAsync();
              
                foreach(var facility in facilities)
                {
                    _context.AdFacility.Add(new AdFacility { FacilityId = facility.FacilityId, AdId=advertisement.AdId });
                }

                await _context.SaveChangesAsync();
                return Created("Advertisement Added", new { id = advertisement.AdId});
                
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
            var advertisement = await _context.Advertisement.Include(a => a.Rent).Include(a => a.Rent.Room).FirstAsync(a => a.AdId == id);
            
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
