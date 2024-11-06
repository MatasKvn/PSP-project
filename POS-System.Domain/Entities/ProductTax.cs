using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductTax")]
    public record ProductTax(int productId, DateTime productVersion, int taxId, DateTime taxVersion)
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Product")]
        public int ProductId { get; init; } = productId;
        [Key, Column(Order = 1)]
        [ForeignKey("Product")]
        public DateTime ProductVersion { get; init; } = productVersion;
        [Key, Column(Order = 2)]
        [ForeignKey("Tax")]
        public int TaxId { get; init; } = taxId;
        [Key, Column(Order = 3)]
        [ForeignKey("Tax")]
        public DateTime TaxVersion { get; init; } = taxVersion;
    }
}
