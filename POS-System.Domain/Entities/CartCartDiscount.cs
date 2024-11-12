using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartCartDiscounts")]
    [PrimaryKey(nameof(CartId), nameof(CartVersion), nameof(CartDiscountId), nameof(CartDiscountVersion))]
    public record CartCartDiscount
    {
        [ForeignKey("Cart")]
        public int CartId { get; init; }
        [ForeignKey("Cart")]
        public DateTime CartVersion { get; init; }
        [ForeignKey("CartDiscount")]
        public int CartDiscountId { get; init; }
        [ForeignKey("CartDiscount")]
        public DateTime CartDiscountVersion { get; init; }
    }
}
