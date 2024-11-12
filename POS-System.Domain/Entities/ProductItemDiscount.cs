using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductItemDiscounts")]
    [PrimaryKey(nameof(ProductId), nameof(ProductVersion), nameof(ItemDiscountId), nameof(ItemDiscountVersion))]
    public record ProductItemDiscount
    {
        [ForeignKey("Product")]
        public int ProductId { get; init; }
        [ForeignKey("Product")]
        public DateTime ProductVersion { get; init; }
        [ForeignKey("ItemDiscount")]
        public int ItemDiscountId { get; init; }
        [ForeignKey("ItemDiscount")]
        public DateTime ItemDiscountVersion { get; init; }
    }
}
