using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("GiftCardDetails")]
    public record GiftCardDetails
    {
        //Primary key
        [Key]
        public int Id { get; set; }

        //Foreign keys
        [ForeignKey("GiftCard")]
        public int GiftCardId { get; set; }
    }
}
