using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Taxes")]
    [PrimaryKey(nameof(Id), nameof(Version))]
    public record Tax
    {
        public int Id { get; init; }
        [MaxLength(64)]
        public required string Name { get; init; }
        public int Rate { get; init; }
        public bool IsPercentage { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }
        public List<Service> Services { get; } = [];
        public List<Product> Products { get; } = [];
        public List<ServiceTax> ServiceTaxes { get; } = [];
        public List<ProductTax> ProductTaxes { get; } = [];
    }
}
