using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Carts")]
    public record Cart
    {
        //Primary key
        [Key]
        public int Id { get; set; }

        //Foreign keys
        [ForeignKey("Employee")]
        public int EmployeeVersionId { get; set; }

        //Navigation properties
        public virtual ICollection<CartOnCartDiscount> CartOnCartDiscounts { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }

        //Fields
        public required DateTime DateCreated { get; set; }
    }
}
