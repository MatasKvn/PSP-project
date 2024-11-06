using POS_System.Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Transactions")]
    public record Transaction(DateTime id, int amount, int tip, string transactionRef, TransactionStatusEnum status, int cardId, int cardDetailsId)
    {
        [Key]
        public DateTime Id { get; set; } = id;
        public int Amount { get; set; } = amount;
        public int Tip { get; set; } = tip;
        [MaxLength(18)]
        public string TransactionRef { get; set; } = transactionRef;
        public TransactionStatusEnum Status { get; set; } = status;
        [ForeignKey("CardDetails")]
        public int CardId { get; set; } = cardId;
        [ForeignKey("GiftCardDetails")]
        public int CardDetailsId { get; set;} = cardDetailsId;
    }
}
