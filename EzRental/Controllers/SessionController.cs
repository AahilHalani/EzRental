using EzRental.Data;
using EzRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using EzRental.Services;
using Microsoft.AspNetCore.Http;
using MySqlX.XDevAPI;
using System.Web;

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
        [HttpPost]
        public async Task<ActionResult> Login(Credentials credentials)
        {
            Console.WriteLine("login Hit");
            if (_context.Credentials == null || credentials == null)
            {
                return Problem("Server ran into an unexpected problem.");
            }

            try
            {
                var user_credentials = await _context.Credentials.SingleOrDefaultAsync(c => c.Username == credentials.Username);

                if (user_credentials == null)
                    return NotFound("User Does not Exists");

                if (_passwordHashser.Verify(user_credentials.Password, credentials.Password, user_credentials.Salt))
                {
                    HttpContext.Response.Cookies.Append("user", user_credentials.Username,
                        new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            Expires = DateTime.Now.AddHours(1),
                            Secure = true
                        });

                    var userId = _context.Credentials.Where(u => u.Username == credentials.Username).
                        Select(u => u.UserId).FirstAsync().Result;

                    HttpContext.Response.Cookies.Append("userId", userId.ToString(),
                        new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            Expires = DateTime.Now.AddHours(1),
                            Secure = true
                        });

                    return Ok(new { message = "login Succesful" });
                }

                else
                    return NotFound(new { message = "Incorrect Credentials" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(new { message = "Server Error"});
            }
        }

        // POST: credential - user signup
        // adds user credentials to the database
        [HttpPost]
        public async Task<ActionResult> Signup(CredentialWrapper credentialUserData)
        {
            try
            {
                Credentials credentials = (credentialUserData.credentials != null) ?
                    credentialUserData.credentials : throw new ArgumentNullException();

                User user = (credentialUserData.user != null) ?
                    credentialUserData.user : throw new ArgumentNullException();

                var hashCredentials = _passwordHashser.Hash(credentials.Password);

                credentials.Password = hashCredentials[0];
                credentials.Salt = hashCredentials[1];

                _context.User.Add(user);
                await _context.SaveChangesAsync();

                credentials.UserId = user.UserId;
                _context.Credentials.Add(credentials);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Login", new { user = credentials.Username });
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.ToString());
                return Problem("Server ran into an unexpected problem");
            }
            catch
            {
                return Problem("Server ran into an unexpected error.");
            }
        }

        [HttpGet]
        public Task<ActionResult> Logout()
        {
            Response.Cookies.Delete("user");
            Response.Cookies.Delete("userId");
            return Task.FromResult<ActionResult>(Ok("User Logged Out"));
        }

        [HttpGet]
        public async Task<ActionResult> VerifySession()
        {
            var user_id = HttpContext.Request.Cookies["userId"];
            if(user_id == null)
            {
                return NotFound(new { message="Session Terminated"});
            }
            int.TryParse(user_id, out var userId);
            bool userExist = await UserExistsAsync(userId);
            if(!userExist)
            {
                return NotFound(new { message="Session Terminated"});
            }
            var userName = await _context.Credentials.Where(c => c.UserId == userId).Select(c=>c.Username).FirstOrDefaultAsync();

            return Ok(new {id = user_id, name = userName});
        }

        private async Task<bool> UserExistsAsync(int id)
        {
            return await _context.User.AnyAsync(e => e.UserId == id);
        }

    }
}
