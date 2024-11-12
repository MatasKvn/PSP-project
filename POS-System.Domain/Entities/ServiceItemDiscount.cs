using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ServiceItemDiscounts")]
    [PrimaryKey(nameof(ServiceId), nameof(ServiceVersion), nameof(ItemDiscountId), nameof(ItemDiscountVersion))]
    public record ServiceItemDiscount
    {
        [ForeignKey("Service")]
        public int ServiceId { get; init; }
        [ForeignKey("Service")]
        public DateTime ServiceVersion { get; init; }
        [ForeignKey("ItemDiscount")]
        public int ItemDiscountId { get; init; }
        [ForeignKey("ItemDiscount")]
        public DateTime ItemDiscountVersion { get; init; }
    }
}
