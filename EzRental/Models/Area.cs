using Org.BouncyCastle.Asn1.Mozilla;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EzRental.Models
{
    public class Area
    {
        [Key]
        public int AreaId { get; set; }

        [Required]
        [Column("AreaName", TypeName = "nvarchar(50)")]
        public string AreaName { get; set; }

        [Required]
        [Column("CityId", TypeName = "int")]
        public int CityId { get; set; }
        public City? city { get; set; }
    }
}
