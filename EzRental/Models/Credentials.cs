using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EzRental.Models
{
    public class Credentials
    {
        [Key]
        public int CredentialId { get; set; }

        [Required]
        [Column("UserId", TypeName = "int")]
        public int UserId { get; set; }

        [Required]
        [Column("Username", TypeName = "nvarchar(50)")]
        public string Username { get; set; }

        [Required]
        [Column("Password", TypeName = "nvarchar(50)")]
        public string Password { get; set; }

        [Column("Salt", TypeName = "nvarchar(80)")]
        public string? Salt { get; set; }
    }
}
