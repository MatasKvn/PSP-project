using POS_System.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Transactions")]
    public record Transaction
    {
        //Primary key
        [Key]
        public DateTime Id { get; init; }

        //Foreign keys
        [ForeignKey("CardDetails")]
        public int CardId { get; init; }
        [ForeignKey("GiftCardDetails")]
        public int CardDetailsId { get; init; }

        //Navigation properties
        public virtual Cart Cart { get; set; }

        //Fields
        public required int Amount { get; init; }
        public required int Tip { get; init; }
        [MaxLength(18)]
        public required string TransactionRef { get; init; }
        public required TransactionStatusEnum Status { get; init; }
    }
}
