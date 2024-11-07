using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartItems")]
    public record CartItem
    {
        [Key]
        public int Id { get; init; }
        public int CartId { get; init; }
        public int Quantity { get; init; }

        [ForeignKey("Product")]
        public int? ProductId { get; init; }
        [ForeignKey("Product")]
        public DateTime? ProductVersion { get; init; }

        [ForeignKey("Service")]
        public int? ServiceId { get; init; }
        [ForeignKey("Service")]
        public DateTime? ServiceVersion { get; init; }

        public bool IsProduct { get; init; }
    }
}
