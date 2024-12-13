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
        
        //Navigation properties
        public virtual Cart Cart { get; set; }

        //Fields
        public int CartId { get; set; }
        public required ulong Amount { get; set; }
        public int? Tip { get; set; }
        public required string TransactionRef { get; set; }
        public required TransactionStatusEnum Status { get; set; }
    }
}
