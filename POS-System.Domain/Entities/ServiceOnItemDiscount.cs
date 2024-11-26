using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ServiceOnItemDiscounts")]
    [PrimaryKey(nameof(ServiceVersionId), nameof(ItemDiscountVersionId), nameof(StartDate))]
    public record ServiceOnItemDiscount
    {
        //Primary key
        [ForeignKey("Service")]
        public int ServiceVersionId { get; set; }
        [ForeignKey("ItemDiscount")]
        public int ItemDiscountVersionId { get; set; }
        public DateTime StartDate { get; set; }
        public required DateTime? EndDate { get; set; }

        //Navigation properties
        public virtual Service Service { get; set; }
        public virtual ItemDiscount ItemDiscount { get; set; }
    }
}
