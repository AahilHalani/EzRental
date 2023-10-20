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
    public class RentController : ControllerBase
    {
        private readonly EzRentalDbContext _context;

        public RentController(EzRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/Rent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rent>>> GetRent()
        {
          if (_context.Rent == null)
          {
              return NotFound();
          }
            return await _context.Rent.ToListAsync();
        }

        // GET: api/Rent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rent>> GetRent(int id)
        {
          if (_context.Rent == null)
          {
              return NotFound();
          }
            var rent = await _context.Rent.FindAsync(id);

            if (rent == null)
            {
                return NotFound();
            }

            return rent;
        }

        // PUT: api/Rent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRent(int id, Rent rent)
        {
            if (id != rent.RentId)
            {
                return BadRequest();
            }

            _context.Entry(rent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentExists(id))
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

        // POST: api/Rent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Rent>> PostRent(Rent rent)
        {
          if (_context.Rent == null)
          {
              return Problem("Entity set 'EzRentalDbContext.Rent'  is null.");
          }
            _context.Rent.Add(rent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRent", new { id = rent.RentId }, rent);
        }

        // DELETE: api/Rent/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRent(int id)
        {
            if (_context.Rent == null)
            {
                return NotFound();
            }
            var rent = await _context.Rent.FindAsync(id);
            if (rent == null)
            {
                return NotFound();
            }

            _context.Rent.Remove(rent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentExists(int id)
        {
            return (_context.Rent?.Any(e => e.RentId == id)).GetValueOrDefault();
        }
    }
}
