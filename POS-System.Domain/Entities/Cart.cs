using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POS_System.Common.Enums;
using POS_System.Domain.Entities.Interfaces;

namespace POS_System.Domain.Entities
{
    [Table("Carts")]
    public record Cart : ILinkable
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
        public required DateTime DateCreated { get; init; }
        //Not used currently as entity is not versioned
        //Need to keep it to implement ILinkable for generic many to many logic
        public required bool IsDeleted { get; set; }
        public required CartStatusEnum Status { get; set; }
    }
}
