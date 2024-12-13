using POS_System.Domain.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartDiscounts")]
    public record CartDiscount
    {
        //Primary key
        [Key]
        public string Id { get; set; }

        //Fields
        public required int Value { get; set; }
        public required bool IsPercentage { get; set; }
        public required string Description { get; set; }
        public required DateTime? StartDate { get; set; }
        public required DateTime? EndDate { get; set; }

        //Navigation properties
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
