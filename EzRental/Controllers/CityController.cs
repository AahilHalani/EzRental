using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EzRental.Data;
using EzRental.Models;
using NuGet.Versioning;

namespace EzRental.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly EzRentalDbContext _context;

        public CityController(EzRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/City
        [HttpGet]

        public async Task<ActionResult<IEnumerable<City>>> GetCity()
        {
            if (_context.City == null)
            {
                return NotFound();
            }

            var cities = await _context.City.Include(c => c.Country).ToListAsync();
            
            return Ok(cities); 
        }

        // GET: api/City/5
        [HttpGet("{id}")]
        public ActionResult<City> GetCity(int id)
        {
          if (_context.City == null)
          {
              return NotFound();
          }
            
            var city =  _context.City.Include(c => c.Country).FirstOrDefault(c => c.CityId == id);

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        // PUT: api/City/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, City city)
        {
            if (id != city.CityId)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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

        // POST: api/City
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(City city)
        {
          if (_context.City == null)
          {
              return Problem("Entity set 'EzRentalDbContext.City'  is null.");
          }
            _context.City.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.CityId }, city);
        }

        // DELETE: api/City/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            if (_context.City == null)
            {
                return NotFound();
            }
            var city = await _context.City.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.City.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return (_context.City?.Any(e => e.CityId == id)).GetValueOrDefault();
        }
    }
}
