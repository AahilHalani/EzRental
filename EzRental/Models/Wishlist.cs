using System.ComponentModel.DataAnnotations.Schema;

namespace EzRental.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        
        [Column("UserId", TypeName = "int")]
        public int UserId { get; set; }
        public User? User { get; set; }
        
        [Column("AdId", TypeName = "int")]
        public int AdvertisementId { get; set; }
        public Advertisement? Advertisement { get; set; }
    }
}
