using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EzRental.Models
{
    public class Rent
    {
        [Key]
        public int RentId { get; set; }

        [Required]
        [Column("RoomId", TypeName = "int")]
        public int RoomId { get; set; }
        public Room? Room { get; set; } //Navigational Property

        [Required]
        [Column("RenterId", TypeName = "int")]
        public int RenterId { get; set; }

        public User? Renter { get; set; }
    }
}
