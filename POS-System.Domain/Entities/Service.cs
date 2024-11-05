using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_System.Domain.Entities
{
    [Table("Services")]
    public class Service(int id, string name, string description, int duration, int price, string imageURL, int version, bool isDeleted = false)
    {
        [Key]
        public int Id { get; } = id;
        [Required]
        [MaxLength(40)]
        public string Name { get; } = name;
        [Required]
        public string Description { get; } = description;
        [Required]
        public int Duration { get; } = duration; //In minutes?
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
