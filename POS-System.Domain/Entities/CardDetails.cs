using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CardDetails")]
    public record CardDetails
    {
        [Key]
        public DateTime Id { get; init; }
        [MaxLength(70)]
        public required string HolderName { get; init; }
        [MaxLength(4)]
        public required string ExpireDate { get; init; }
        [MaxLength(4)]
        public required string CardDigits { get; init; }
    }
}
