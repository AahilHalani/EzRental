using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EzRental.Models
{
    public class Advertisement
    {
        [Key]
        public int AdId { get; set; }

        [Required]
        [Column("RentId", TypeName = "int")]
        public int RentId { get; set; }
        public Rent? Rent { get; set; }

        [Required]
        [Column("Price", TypeName = "decimal")]
        public decimal Price { get; set; }

        [Required]
        [Column("Description", TypeName = "nvarchar(200)")]
        public string? Description { get; set; }

        [Required]
        [Column("StartDate", TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column("EndDate", TypeName = "date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Column("Area", TypeName = "varchar(50)")]
        public string? Area { get; set; }

        [Required]
        [Column("City", TypeName = "varchar(50)")]
        public string? City { get; set; }

        [Required]
        [Column("Country", TypeName = "varchar(50)")]
        public string? Country { get; set; }
        
    }
}
