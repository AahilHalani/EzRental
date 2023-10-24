using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EzRental.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [Required]
        [Column("CityName", TypeName = "nvarchar(50)")]
        public string CityName { get; set; }

        [Required]
        [Column("CountryId", TypeName = "int")]
        public int CountryId { get; set; }
        public Country? Country { get; set; }   //Navigational Property
    }
}
