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
    public class CredentialController : ControllerBase
    {
        private readonly EzRentalDbContext _context;

        public CredentialController(EzRentalDbContext context)
        {
            _context = context;
        }

        // GET: api/Credential
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Credentials>>> GetCredentials()
        {
          if (_context.Credentials == null)
          {
              return NotFound();
          }
            return await _context.Credentials.ToListAsync();
        }

        // GET: api/Credential/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Credentials>> GetCredentials(int id)
        {
          if (_context.Credentials == null)
          {
              return NotFound();
          }
            var credentials = await _context.Credentials.FindAsync(id);

            if (credentials == null)
            {
                return NotFound();
            }

            return credentials;
        }

        // PUT: api/Credential/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCredentials(int id, Credentials credentials)
        {
            if (id != credentials.CredentialId)
            {
                return BadRequest();
            }

            _context.Entry(credentials).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CredentialsExists(id))
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

        // POST: api/Credential
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Credentials>> PostCredentials(Credentials credentials)
        {
          if (_context.Credentials == null)
          {
              return Problem("Entity set 'EzRentalDbContext.Credentials'  is null.");
          }
            _context.Credentials.Add(credentials);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCredentials", new { id = credentials.CredentialId }, credentials);
        }

        // DELETE: api/Credential/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCredentials(int id)
        {
            if (_context.Credentials == null)
            {
                return NotFound();
            }
            var credentials = await _context.Credentials.FindAsync(id);
            if (credentials == null)
            {
                return NotFound();
            }

            _context.Credentials.Remove(credentials);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CredentialsExists(int id)
        {
            return (_context.Credentials?.Any(e => e.CredentialId == id)).GetValueOrDefault();
        }
    }
}
