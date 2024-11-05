using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductModificationCartItems")]
    public class ProductModificationCartItem(int cartItemId, int productModificationId)
    {
        [Key, Column(Order = 0)]
        public int CartItemId { get; } = cartItemId;
        [Key, Column(Order = 1)]
        public int ProductModificationId { get; } = productModificationId;
    }
}
