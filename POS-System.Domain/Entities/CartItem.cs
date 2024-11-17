using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("CartItems")]
    public record CartItem
    {
        //Primary key
        [Key]
        public int Id { get; init; }

        //Foreign keys
        [ForeignKey("Product")]
        public int? ProductVersionId { get; init; }

        [ForeignKey("Service")]
        public int? ServiceVersionId { get; init; }

        //Navigational properties
        public virtual ICollection<ProductModificationOnCartItem> ProductModificationsOnCartItem { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual ServiceReservation ServiceReservation { get; set; }

        //Fields
        public int CartId { get; init; }
        public required int Quantity { get; init; }
        public required bool IsProduct { get; init; }
    }
}
