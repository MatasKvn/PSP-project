using POS_System.Domain.Entities.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartItems")]
    public record CartItem : ILinkable
    {
        //Primary key
        [Key]
        public int Id { get; set; }

        //Foreign keys
        [ForeignKey("Product")]
        public int? ProductVersionId { get; set; }

        [ForeignKey("Service")]
        public int? ServiceVersionId { get; set; }

        //Navigational properties
        public virtual ICollection<ProductModificationOnCartItem> ProductModificationsOnCartItem { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ServiceReservation ServiceReservation { get; set; }

        //Fields
        public int CartId { get; set; }
        public required int Quantity { get; set; }
        public required bool IsProduct { get; set; }

        //Not used currently as entity is not versioned
        //Need to keep it to implement ILinkable for generic many to many logic
        public required bool IsDeleted { get; set; }
    }
}
