using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EzRental.Models
{
    public class Facilties
    {
        [Key]
        public int FacilityId { get; set; }

        [Required]
        [Column("FacilityName", TypeName = "nvarchar(50)")]
        public string? FacilityName { get; set; }
    }
}
