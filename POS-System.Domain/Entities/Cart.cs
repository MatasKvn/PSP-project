using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POS_System.Common.Enums;

namespace POS_System.Domain.Entities
{
    [Table("Carts")]
    public record Cart
    {
        //Primary key
        [Key]
        public int Id { get; init; }

        //Foreign keys
        [ForeignKey("Employee")]
        public int EmployeeVersionId { get; init; }

        //Navigation properties
        public virtual ICollection<CartOnCartDiscount> CartOnCartDiscounts { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        //Fields
        public required DateTime DateCreated { get; init; }
        public required CartStatusEnum Status { get; set; }
    }
}
