using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EzRental.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        [Column("AdId", TypeName = "int")]
        public int AdId { get; set; }

        [Required]
        [Column("ProposalId", TypeName = "int")]
        public int ProposalId { get; set; }
        
    }
}
