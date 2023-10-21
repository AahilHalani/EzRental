using EzRental.Data;
using EzRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using EzRental.Services;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EzRental.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly EzRentalDbContext _context;
        private readonly PasswordHasher _passwordHashser;

        public SessionController(EzRentalDbContext context)
        {
            _context = context;
            _passwordHashser = new PasswordHasher();
        }


        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult Login(Credentials credentials)
        {
            Console.WriteLine("login Hit");
            if (_context.Credentials == null || credentials == null)
            {
                return Problem("Server ran into an unexpected problem.");
            }

            try
            {
                var user_credentials = _context.Credentials.SingleOrDefault(c => c.Username == credentials.Username);

                if (user_credentials == null)
                    return NotFound("User Does not Exists");

                if (_passwordHashser.Verify(user_credentials.Password, credentials.Password, user_credentials.Salt))
                {
                    HttpContext.Session.SetString("SessionUser", user_credentials.Username);
                    return Ok("login Succesful");
                }

                else
                    return NotFound("Incorrect Credentials");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }


        // POST: credential - user signup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // adds user credentials to the database
        [HttpPost]
        public async Task<ActionResult> Signup(Credentials credentials)
        {
            try
            {
                var hashCredentials = _passwordHashser.Hash(credentials.Password);

                credentials.Password = hashCredentials[0];
                credentials.Salt = hashCredentials[1];

                _context.Credentials.Add(credentials);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCredentials", new { id = credentials.CredentialId });
            }
            catch
            {
                return Problem("Server ran into an unexpected error.");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            if (HttpContext.Session.GetString("SessionUser") != null)
            {
                HttpContext.Session.Remove("SessionUser");
            }

            return Ok("User Logged Out");
        }

    }
}
