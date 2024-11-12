using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductModifications")]
    [PrimaryKey(nameof(Id), nameof(Version))]
    public record ProductModification
    {
        public int Id { get; init; }
        [ForeignKey("Product")]
        public int ProductId { get; init; }
        [ForeignKey("Product")]
        public DateTime ProductVersion { get; init; }
        [MaxLength(40)]
        public required string Name { get; init; }
        public required string Description { get; init; }
        public int Price { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }
    }
}
