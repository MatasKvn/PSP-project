using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Taxes")]
    public record Tax
    {
        //Priamry key
        [Key]
        public int Id { get; set; }

        //Navigation properties
        public virtual ICollection<ProductOnTax> ProductOnTaxes { get; set; }
        public virtual ICollection<ServiceOnTax> ServiceOnTaxes { get; set; }


        //Fields
        public int TaxId { get; set; }
        [MaxLength(64)]
        public required string Name { get; set; }
        public required int Rate { get; set; }
        public required bool IsPercentage { get; set; }

        //Versioning
        public DateTime Version { get; set; }
        public bool IsDeleted { get; set; }
    }
}
