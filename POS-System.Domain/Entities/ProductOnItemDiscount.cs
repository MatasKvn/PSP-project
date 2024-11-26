using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductOnItemDiscounts")]
    [PrimaryKey(nameof(ProductVersionId), nameof(ItemDiscountVersionId), nameof(StartDate))]
    public record ProductOnItemDiscount
    {
        //Primary key
        [ForeignKey("Product")]
        public int ProductVersionId { get; set; }
        [ForeignKey("ItemDiscount")]
        public int ItemDiscountVersionId { get; set; }
        public DateTime StartDate { get; set; }
        public required DateTime? EndDate { get; set; }

        //Navigation properties
        public virtual Product Product { get; set; }
        public virtual ItemDiscount ItemDiscount { get; set; }
    }
}
