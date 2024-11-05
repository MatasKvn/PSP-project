using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("ProductModifications")]
    public class ProductModification(int id, int productId, string name, string description, int price, int version, bool isDeleted = false)
    {
        [Key]
        public int Id { get; } = id;
        [Required]
        public int ProductId { get; } = productId;
        [Required]
        [MaxLength(40)]
        public string Name { get; } = name;
        [Required]
        public string Description { get; } = description;
        [Required]
        public int Price { get; } = price;

        //Versioning
        [Required]
        public int Version { get; } = version;
        [Required]
        public bool IsDeleted { get; } = isDeleted;
    }
}
