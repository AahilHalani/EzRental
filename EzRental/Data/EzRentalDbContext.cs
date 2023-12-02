using EzRental.Models;
using Microsoft.EntityFrameworkCore;

namespace EzRental.Data
{
    public class EzRentalDbContext:DbContext
    {
        public EzRentalDbContext(DbContextOptions options):base(options)
        {
                
        }

        public DbSet<AdFacility> AdFacility { get; set; }
        public DbSet<Advertisement> Advertisement { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Country{ get; set; }
        public DbSet<Credentials> Credentials { get; set; }
        public DbSet<Facilties> Facilities { get; set; }
        public DbSet<Proposal> Proposal { get; set; }
        public DbSet<Rent> Rent { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }

    }
}
