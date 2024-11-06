using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductModificationOnCartItems")]
    public record ProductModificationCartItem(int cartItemId, DateTime cartItemVersion, int productModificationId, DateTime productModificationVersion)
    {
        [Key, Column(Order = 0)]
        [ForeignKey("CartItem")]
        public int CartItemId { get; init; } = cartItemId;
        [Key, Column(Order = 1)]
        [ForeignKey("CartItem")]
        public DateTime CartItemVersion { get; init; } = cartItemVersion;
        [Key, Column(Order = 2)]
        [ForeignKey("ProductModification")]
        public int ProductModificationId { get; init; } = productModificationId;
        [Key, Column(Order = 3)]
        [ForeignKey("ProductModification")]
        public DateTime ProductModificationVersion { get; init; } = productModificationVersion;
    }
}
