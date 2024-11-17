using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ServiceOnItemDiscounts")]
    [PrimaryKey(nameof(ServiceVersionId), nameof(ItemDiscountVersionId))]
    public record ServiceOnItemDiscount
    {
        //Primary key
        [ForeignKey("Service")]
        public int ServiceVersionId { get; init; }
        [ForeignKey("ItemDiscount")]
        public int ItemDiscountVersionId { get; init; }

        //Navigation properties
        public virtual Service Service { get; set; }
        public virtual ItemDiscount ItemDiscount { get; set; }
    }
}
