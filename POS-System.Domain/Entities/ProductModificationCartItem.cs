using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductModificationOnCartItems")]
    [PrimaryKey(nameof(CartItemId), nameof(CartItemVersion), nameof(ProductModificationId), nameof(ProductModificationVersion))]
    public record ProductModificationCartItem
    {
        [ForeignKey("CartItem")]
        public int CartItemId { get; init; }
        [ForeignKey("CartItem")]
        public DateTime CartItemVersion { get; init; }
        [ForeignKey("ProductModification")]
        public int ProductModificationId { get; init; }
        [ForeignKey("ProductModification")]
        public DateTime ProductModificationVersion { get; init; }
    }
}
