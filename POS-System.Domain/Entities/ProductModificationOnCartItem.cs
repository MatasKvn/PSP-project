using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductModificationOnCartItems")]
    [PrimaryKey(nameof(CartItemId), nameof(ProductModificationVersionId))]
    public record ProductModificationOnCartItem
    {
        //Primary key
        [ForeignKey("CartItem")]
        public int CartItemId { get; init; }
        [ForeignKey("ProductModification")]
        public int ProductModificationVersionId { get; init; }

        //Navigation properties
        public virtual CartItem CartItem { get; set; }
        public virtual ProductModification ProductModification { get; set; }
    }
}
