using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Taxes")]
    public record Tax
    {
        //Priamry key
        [Key]
        public int Id { get; init; }

        //Navigation properties
        public virtual ICollection<ProductOnTax> ProductOnTaxes { get; set; }
        public virtual ICollection<ServiceOnTax> ServiceOnTaxes { get; set; }


        //Fields
        public int TaxId { get; init; }
        [MaxLength(64)]
        public required string Name { get; init; }
        public required int Rate { get; init; }
        public required bool IsPercentage { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }
    }
}
