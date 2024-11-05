using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductTax")]
    public record ProductTax(int productId, DateTime productVersion, int taxId, DateTime taxVersion)
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; init; } = productId;
        [Key, Column(Order = 1)]
        public DateTime ProductVersion { get; init; } = productVersion;
        [Key, Column(Order = 2)]
        public int TaxId { get; init; } = taxId;
        [Key, Column(Order = 3)]
        public DateTime TaxVersion { get; init; } = taxVersion;
    }
}
