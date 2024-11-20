using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ItemDiscounts")]
    public record ItemDiscount
    {
        //Primary key
        [Key]
        public int Id { get; set; }

        //Navigation properties
        public virtual ICollection<ProductOnItemDiscount> ProductOnItemDiscounts { get; set; }
        public virtual ICollection<ServiceOnItemDiscount> ServiceOnItemDiscounts { get; set; }

        //Fields
        public required int ItemDiscountId { get; set; }
        public required int Value { get; set; }
        public required bool IsPercentage { get; set; }
        public required string Description { get; set; }
        public required DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        //Versioning
        public DateTime Version { get; set; }
        public bool IsDeleted { get; set; }
    }
}
