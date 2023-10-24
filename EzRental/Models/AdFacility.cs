using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EzRental.Models
{
    public class AdFacility
    {
        [Key]
        public int AdFacilityId { get; set; }

        [Required]
        [Column("FacilityId", TypeName = "int")]
        public int FacilityId { get; set; }
        public Facilties? Facility { get; set; }

        [Required]
        [Column("AdId", TypeName = "int")]
        public int AdId { get; set; }
        public Advertisement? Ad { get; set; }
    }
}
