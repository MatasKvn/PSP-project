using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("GiftCardDetails")]
    public record GiftCardDetails(int id, int giftCardId)
    {
        [Key]
        public int Id { get; set; } = id;
        [ForeignKey("GiftCardDetails")]
        public int GiftCardId { get; set; } = giftCardId;
    }
}
