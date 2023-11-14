using EzRental.Data;
using EzRental.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using EzRental.Services;
using Microsoft.AspNetCore.Http;
using MySqlX.XDevAPI;
using System.Web;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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
                    HttpContext.Response.Cookies.Append("user", user_credentials.Username,
                        new Microsoft.AspNetCore.Http.CookieOptions
                        {
                            Expires = DateTime.Now.AddHours(1)
                        });
                    
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
        // adds user credentials to the database
        [HttpPost]
        public async Task<ActionResult> Signup(CredentialWrapper credentialUserData)
        {
            Console.WriteLine("login Hit");
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

                return CreatedAtAction("Login", new { user = credentials.Username, 
                    message="User successfully created" });
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
        public ActionResult Logout()
        {
            Response.Cookies.Delete("user");
            return Ok("User Logged Out");
        }

    }
}
