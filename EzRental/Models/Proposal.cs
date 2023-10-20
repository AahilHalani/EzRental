using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EzRental.Models
{
    public class Proposal
    {
        [Key]
        public int ProposalId { get; set; }

        [Required]
        [Column("AdId", TypeName = "int")]
        public int AdId { get; set; }

        [Required]
        [Column("RenteeId", TypeName = "int")]
        public int RenteeId { get; set; }

        [Required]
        [Column("UserBackground", TypeName = "nvarchar(300)")]
        public string UserBackground { get; set; }

        [Required]
        [Column("Status", TypeName = "nvarchar(50)")]
        public string Status { get; set; }
    }
}
