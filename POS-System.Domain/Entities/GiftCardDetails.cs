using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("GiftCardDetails")]
    public record GiftCardDetails
    {
        [Key]
        public int Id { get; init; }
        public int GiftCardId { get; init; }
        public GiftCard GiftCard { get; init; } = null!;

    }
}
