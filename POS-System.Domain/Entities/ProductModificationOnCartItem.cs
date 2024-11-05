using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductModificationOnCartItems")]
    public record ProductModificationOnCartItem(int cartItemId, DateTime cartItemVersion, int productModificationId, DateTime productModificationVersion)
    {
        [Key, Column(Order = 0)]
        public int CartItemId { get; init; } = cartItemId;
        [Key, Column(Order = 1)]
        public DateTime CartItemVersion { get; init; } = cartItemVersion;
        [Key, Column(Order = 2)]
        public int ProductModificationId { get; init; } = productModificationId;
        [Key, Column(Order = 3)]
        public DateTime ProductModificationVersion { get; init; } = productModificationVersion;
    }
}
