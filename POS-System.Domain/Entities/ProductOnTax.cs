using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductOnTax")]
    [PrimaryKey(nameof(ProductVersionId), nameof(TaxVersionId))]
    public record ProductOnTax
    {
        //Primary key
        [ForeignKey("Product")]
        public int ProductVersionId { get; init; }
        [ForeignKey("Tax")]
        public int TaxVersionId { get; init; }

        //Navigation properties
        public virtual Product Product { get; set; }
        public virtual Tax Tax { get; set; }
    }
}
