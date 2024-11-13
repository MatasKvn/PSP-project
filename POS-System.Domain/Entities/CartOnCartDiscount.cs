using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartOnCartDiscounts")]
    [PrimaryKey(nameof(CartVersionId), nameof(CartDiscountVersionId))]
    public record CartOnCartDiscount
    {
        //Primary keys
        [ForeignKey("Cart")]
        public int CartVersionId { get; init; }
        [ForeignKey("CartDiscount")]
        public int CartDiscountVersionId { get; init; }

        //Navigation properties
        public virtual Cart Cart { get; set; }
        public virtual CartDiscount CartDiscount { get; set; }
    }
}
