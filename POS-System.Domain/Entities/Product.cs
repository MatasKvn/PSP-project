using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Domain.Entities
{
    [Table("Products")]
    public class Product(int id, string name, string description, int price, string imageURL, int version, bool isDeleted = false)
    {
        [Key]
        public int Id { get; } = id;
        [Required]
        [MaxLength(40)]
        public string Name { get; } = name;
        [Required]
        public string Description { get; } = description;
        [Required]
        public int Price { get; } = price; //We hold prices in cents
        [Required]
        public string ImageURL { get; } = imageURL;

        //Versioning
        [Required]
        public int Version { get; } = version;
        [Required]
        public bool IsDeleted { get; } = isDeleted;
    }
}
