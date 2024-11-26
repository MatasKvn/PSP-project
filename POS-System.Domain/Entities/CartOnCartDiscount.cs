using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartOnCartDiscounts")]
    [PrimaryKey(nameof(CartVersionId), nameof(CartDiscountVersionId), nameof(StartDate))]
    public record CartOnCartDiscount
    {
        //Primary keys
        [ForeignKey("Cart")]
        public int CartVersionId { get; set; }
        [ForeignKey("CartDiscount")]
        public int CartDiscountVersionId { get; set; }
        public DateTime StartDate { get; set; }
        public required DateTime? EndDate { get; set; }

        //Navigation properties
        public virtual Cart Cart { get; set; }
        public virtual CartDiscount CartDiscount { get; set; }
    }
}
