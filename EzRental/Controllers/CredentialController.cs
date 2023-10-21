using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EzRental.Data;
using EzRental.Models;
using System.Security;
using EzRental.Services;
using Newtonsoft.Json;

namespace EzRental.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CredentialController : ControllerBase
    {
        private readonly EzRentalDbContext _context;
        private readonly PasswordHasher _passwordHashser;

        public CredentialController(EzRentalDbContext context)
        {
            _context = context;
            _passwordHashser = new PasswordHasher();
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
