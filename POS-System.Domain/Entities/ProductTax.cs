using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductTax")]
    [PrimaryKey(nameof(ProductId), nameof(ProductVersion), nameof(TaxId), nameof(TaxVersion))]
    public record ProductTax
    {
        public int ProductId { get; init; }
        public DateTime ProductVersion { get; init; }
        public int TaxId { get; init; }
        public DateTime TaxVersion { get; init; }
        public Product Product { get; init; } = null!;
        public Tax Tax { get; init; } = null!;
    }
}
