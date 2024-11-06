using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace POS_System.Domain.Entities
{
    [Table("Products")]
    public record Product(int id, string name, string description, int price, string imageURL, DateTime version, bool isDeleted = false)
    {
        [Key, Column(Order = 0)]
        public int Id { get; init; } = id;
        [MaxLength(40)]
        public string Name { get; init; } = name;
        public string Description { get; init; } = description;
        public int Price { get; init; } = price; //We hold prices in cents
        public string ImageURL { get; init; } = imageURL;

        //Versioning
        [Key, Column(Order = 1)]
        public DateTime Version { get; init; } = version;
        public bool IsDeleted { get; init; } = isDeleted;
    }
}
