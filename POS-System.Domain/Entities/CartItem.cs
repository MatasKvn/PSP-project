using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartItems")]
    public record CartItem(int id, int cartId, int itemId, DateTime itemVersion, bool isProduct = true, int quantity = 1)
    {
        [Key]
        public int Id { get; init; } = id;
        [ForeignKey("Cart")]
        public int CartId { get; init; } = cartId;
        public int Quantity { get; init; } = quantity;

        [ForeignKey("Product")]
        public int? ProductId { get; init; } = isProduct ? itemId : null;
        [ForeignKey("Product")]
        public DateTime? ProductVersion { get; init; } = isProduct ? itemVersion : null;

        [ForeignKey("Service")]
        public int? ServiceId { get; init; } = isProduct ? null : itemId;
        [ForeignKey("Service")]
        public DateTime? ServiceVersion { get; init; } = isProduct ? null : itemVersion;

        public bool IsProduct { get; init; } = isProduct;
    }
}
