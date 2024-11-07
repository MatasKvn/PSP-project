using POS_System.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Transactions")]
    public record Transaction
    {
        [Key]
        public DateTime Id { get; init; }
        public int Amount { get; init; }
        public int Tip { get; init; }
        [MaxLength(18)]
        public required string TransactionRef { get; init; }
        public TransactionStatusEnum Status { get; init; }
        [ForeignKey("CardDetails")]
        public int CardId { get; init; }
        [ForeignKey("GiftCardDetails")]
        public int CardDetailsId { get; init; }
    }
}
