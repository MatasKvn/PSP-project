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
        public DateTime Id { get; set; }

        //Foreign keys
        [ForeignKey("CardDetails")]
        public int CardId { get; set; }
        [ForeignKey("GiftCardDetails")]
        public int CardDetailsId { get; set; }

        //Navigation properties
        public virtual Cart Cart { get; set; }

        //Fields
        public required int Amount { get; set; }
        public required int Tip { get; set; }
        [MaxLength(18)]
        public required string TransactionRef { get; set; }
        public required TransactionStatusEnum Status { get; set; }
    }
}
