using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductOnItemDiscounts")]
    [PrimaryKey(nameof(ProductVersionId), nameof(ItemDiscountVersionId))]
    public record ProductOnItemDiscount
    {
        //Primary key
        [ForeignKey("Product")]
        public int ProductVersionId { get; init; }
        [ForeignKey("ItemDiscount")]
        public int ItemDiscountVersionId { get; init; }

        //Navigation properties
        public virtual Product Product { get; set; }
        public virtual ItemDiscount ItemDiscount { get; set; }
    }
}
