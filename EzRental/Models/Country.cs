using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EzRental.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        [Column("CountryName", TypeName = "nvarchar(50)")]
        public string CountryName { get; set; }
    }
}
