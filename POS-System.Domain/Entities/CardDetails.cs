using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CardDetails")]
    public record CardDetails
    {
        //Primary key
        [Key]
        public DateTime Id { get; set; }

        //Fields
        [MaxLength(70)]
        public required string HolderName { get; set; }
        [MaxLength(4)]
        public required string ExpireDate { get; set; }
        [MaxLength(4)]
        public required string CardDigits { get; set; }
    }
}
