using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace POS_System.Domain.Entities
{
    [Table("GiftCards")]
    public record GiftCard
    {
        [Key]
        public int Id { get; init; }
        public DateTime Date { get; init; }
        public int Value { get; init; }
        [MaxLength(8)]
        public required string Code { get; init; }
        public ICollection<GiftCardDetails> GiftCardDetails { get; } = new List<GiftCardDetails>();
    }
}
