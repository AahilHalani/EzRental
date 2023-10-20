using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EzRental.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        
        [Required]
        [Column("FirstName", TypeName = "nvarchar(50)")]
        public string? FirstName { get; set; }

        [Column("LastName", TypeName = "nvarchar(50)")]
        public string? LastName { get; set; }

        [Required]
        [Column("Address", TypeName = "nvarchar(50)")]
        public string? Address { get; set; }

        [Required]
        [Column("PhoneNumber", TypeName = "nvarchar(20)")]
        public string? PhoneNumber { get; set; }

        [Required]
        [Column("Email", TypeName = "nvarchar(50)")]
        public string? Email { get; set; }
    }
}
