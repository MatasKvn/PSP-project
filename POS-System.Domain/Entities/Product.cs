using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Domain.Entities
{
    [Table("Products")]
    [PrimaryKey(nameof(Id), nameof(Version))]
    public record Product
    {
        public int Id { get; init; }
        [MaxLength(40)]
        public required string Name { get; init; }
        public required string Description { get; init; }
        public int Price { get; init; }
        public required string ImageURL { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }
        public List<Tax> Taxes { get; } = [];
        public List<ProductTax> ProductTaxes { get; } = [];

    }
}
