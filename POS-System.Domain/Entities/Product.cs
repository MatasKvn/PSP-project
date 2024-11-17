using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Products")]
    public record Product
    {
        //Primary key
        [Key]
        public int Id { get; init; }

        //Navigation properties
        public virtual ICollection<ProductOnTax> ProductOnTaxes { get; set; }
        public virtual ICollection<ProductOnItemDiscount> ProductOnItemDiscounts { get; set; }
        public virtual ICollection<ProductModification> ProductModifications { get; set; }

        //Fields
        public int ProductId { get; init; }
        [MaxLength(40)]
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required int Price { get; init; }
        public required string ImageURL { get; init; }
        public required int Stock { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }

    }
}
