using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductModificationOnCartItems")]
    [PrimaryKey(nameof(CartItemId), nameof(ProductModificationVersionId), nameof(StartDate))]
    public record ProductModificationOnCartItem
    {
        //Primary key
        [ForeignKey("CartItem")]
        public int CartItemId { get; set; }
        [ForeignKey("ProductModification")]
        public int ProductModificationVersionId { get; set; }
        public DateTime StartDate { get; set; }
        public required DateTime? EndDate { get; set; }

        //Navigation properties
        public virtual CartItem CartItem { get; set; }
        public virtual ProductModification ProductModification { get; set; }
    }
}
