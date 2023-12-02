using EzRental.Data;
using EzRental.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Org.BouncyCastle.Bcpg;

namespace EzRental.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly EzRentalDbContext _context;
        public WishlistController(EzRentalDbContext context) 
        { 
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult> GetWishList(int userId)
        {
            try
            {
                var advertisements = await _context.Wishlist.Include(w => w.Advertisement).
                    Where(w => w.UserId == userId).Select(w => w.Advertisement).ToListAsync();

                List<AdvertisementWrapper> wishList = new List<AdvertisementWrapper>();

                foreach (var _advertisement in advertisements)
                {

                    if (_advertisement == null) continue;

                    AdvertisementWrapper advertisementWrapper = new AdvertisementWrapper();
                    advertisementWrapper.advertisement = _advertisement;
                    advertisementWrapper.facilties = new List<Facilties>();

                    var facilities = _context.AdFacility.Include(af => af.Facility).Where(af => af.AdId == _advertisement.AdId).ToList();

                    if (facilities != null)
                        foreach (var facility in facilities)
                        {
                            if (facility.Facility != null)
                                advertisementWrapper.facilties.Add(facility.Facility);
                        }

                    wishList.Add(advertisementWrapper);
                }


                return Ok(wishList);
            }
            catch (Exception ex)
            {
                return Problem("Server ran into an error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddWishList(Wishlist wishlist)
        {
            try
            {
                wishlist.User = null;
                wishlist.Advertisement = null;

                await _context.Wishlist.AddAsync(wishlist);
                int id = _context.SaveChanges();

                return Created($"wishlist/{id}", new { message = "Advertisement added to user's wishlist" });
            }
            catch(Exception ex) 
            {
                return Problem("Server ran into an error");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteWishList(Wishlist wishlist)
        {
            try
            {
                wishlist.Id = await _context.Wishlist
                    .Where(w => w.AdvertisementId == wishlist.AdvertisementId && w.UserId == wishlist.UserId)
                    .Select(w => w.Id).FirstAsync();

                _context.Wishlist.Remove(wishlist);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Advertisement removed from wishlist" });
            }
            catch(Exception ex)
            {
                return Problem("Server ran into an error");
            }
        }
    }
}
