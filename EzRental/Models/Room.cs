using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EzRental.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        [Column("Type", TypeName = "nvarchar(50)")]
        public string? Type { get; set; }

        [Required]
        [Column("NumBeds", TypeName = "int")]
        public int NumBeds { get; set; }

        [Required]
        [Column("SqArea", TypeName = "decimal")]
        public decimal? SqArea { get; set; }

        [Column("PicturePath", TypeName = "nvarchar(100)")]
        public string? PicturePath { get; set; }
    }
}
