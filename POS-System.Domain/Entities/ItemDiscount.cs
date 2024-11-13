using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ItemDiscounts")]
    public record ItemDiscount
    {
        //Primary key
        [Key]
        public int Id { get; init; }

        //Navigation properties
        public virtual ICollection<ProductOnItemDiscount> ProductOnItemDiscounts { get; set; }
        public virtual ICollection<ServiceOnItemDiscount> ServiceOnItemDiscounts { get; set; }

        //Fields
        public required int ItemDiscountId { get; init; }
        public required int Value { get; init; }
        public required bool IsPercentage { get; init; }
        public required string Description { get; init; }
        public required DateTime StartDate { get; init; }
        public DateTime? EndDate { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }
    }
}
