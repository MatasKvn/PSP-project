using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductModifications")]
    public record ProductModification(int id, int productId, DateTime productVersion, string name, string description, int price, DateTime version, bool isDeleted = false)
    {
        [Key, Column(Order = 0)]
        public int Id { get; init; } = id;
        [ForeignKey("Product")]
        public int ProductId { get; init; } = productId;
        [ForeignKey("Product")]
        public DateTime ProductVersion { get; init; } = productVersion;
        [MaxLength(40)]
        public string Name { get; init; } = name;
        public string Description { get; init; } = description;
        public int Price { get; init; } = price;

        //Versioning
        [Key, Column(Order = 1)]
        public DateTime Version { get; init; } = version;
        public bool IsDeleted { get; init; } = isDeleted;
    }
}
