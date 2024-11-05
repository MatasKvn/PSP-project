using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Services")]
    public record Service(int id, string name, string description, int duration, int price, string imageURL, DateTime version, bool isDeleted = false)
    {
        [Key, Column(Order = 0)]
        public int Id { get; init; } = id;
        [MaxLength(40)]
        public string Name { get; init; } = name;
        public string Description { get; init; } = description;
        public int Duration { get; init; } = duration; //In minutes?
        public int Price { get; init; } = price; //We hold prices in cents
        public string ImageURL { get; init; } = imageURL;

        //Versioning
        [Key, Column(Order = 1)]
        public DateTime Version { get; init; } = version;
        public bool IsDeleted { get; init; } = isDeleted;
    }
}
