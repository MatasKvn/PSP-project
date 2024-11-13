using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductModifications")]
    public record ProductModification
    {
        //Primary key
        [Key]
        public int Id { get; init; }

        //Foreign keys
        [ForeignKey("Product")]
        public int ProductVersionId { get; init; }

        //Navigation properties
        public virtual Product Product { get; set; }
        public virtual ProductModificationOnCartItem ProductModificationOnCartItems { get; set; }

        //Fields
        public int ProductModificationId { get; init; }
        [MaxLength(40)]
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required int Price { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }
    }
}
