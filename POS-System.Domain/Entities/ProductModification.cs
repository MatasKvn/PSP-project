using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductModifications")]
    public record ProductModification
    {
        //Primary key
        [Key]
        public int Id { get; set; }

        //Foreign keys
        [ForeignKey("Product")]
        public int ProductVersionId { get; set; }

        //Navigation properties
        public virtual Product Product { get; set; }
        public virtual ProductModificationOnCartItem ProductModificationOnCartItems { get; set; }

        //Fields
        public int ProductModificationId { get; set; }
        [MaxLength(40)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }

        //Versioning
        public DateTime Version { get; set; }
        public bool IsDeleted { get; set; }
    }
}
