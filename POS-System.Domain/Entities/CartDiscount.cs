using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartDiscounts")]
    public record CartDiscount
    {
        //Primary key
        [Key]
        public int Id { get; init; }

        //Fields
        public int CartDiscountId { get; init; }
        public required int Value { get; init; }
        public required bool IsPercentage { get; init; }
        public required string Description { get; init; }
        public required DateTime StartDate { get; init; }
        public required DateTime? EndDate { get; init; }

        //Navigation properties
        public virtual ICollection<CartOnCartDiscount> CartOnCartDiscounts { get; set; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }
    }
}
