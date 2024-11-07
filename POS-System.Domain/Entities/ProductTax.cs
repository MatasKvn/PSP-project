using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductTax")]
    [PrimaryKey(nameof(ProductId), nameof(ProductVersion), nameof(TaxId), nameof(TaxVersion))]
    public record ProductTax
    {
        [ForeignKey("Product")]
        public int ProductId { get; init; }
        [ForeignKey("Product")]
        public DateTime ProductVersion { get; init; }
        [ForeignKey("Tax")]
        public int TaxId { get; init; }
        [ForeignKey("Tax")]
        public DateTime TaxVersion { get; init; }
    }
}
