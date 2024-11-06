
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CardDetails")]
    public record CardDetails(DateTime id, string holderName, string expireDate, string cardDetails)
    {
        [Key]
        public DateTime Id { get; set; } = id;
        [MaxLength(70)]
        public string HolderName { get; set; } = holderName;
        [MaxLength(4)]
        public string ExpireDate { get; set; } = expireDate;
        [MaxLength(4)]
        public string CardDigits { get; set; } = cardDetails;
    }
}
