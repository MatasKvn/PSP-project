using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductOnTax")]
    [PrimaryKey(nameof(ProductVersionId), nameof(TaxVersionId), nameof(StartDate))]
    public record ProductOnTax
    {
        //Primary key
        [ForeignKey("Product")]
        public int ProductVersionId { get; set; }
        [ForeignKey("Tax")]
        public int TaxVersionId { get; set; }
        public DateTime StartDate { get; set; }
        public required DateTime? EndDate { get; set; }

        //Navigation properties
        public virtual Product Product { get; set; }
        public virtual Tax Tax { get; set; }
    }
}
