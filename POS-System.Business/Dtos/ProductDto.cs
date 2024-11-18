using System.ComponentModel.DataAnnotations;

namespace POS_System.Business.Dtos
{
    public class ProductDto
    {
        //Primary key
        [Key]
        public int Id { get; init; }

        //Fields
        public int ProductId { get; init; }
        [MaxLength(40)]
        public required string Name { get; init; }
        public required string Description { get; init; }
        public required int Price { get; init; }
        public required string ImageURL { get; init; }
        public required int Stock { get; init; }

        //Versioning
        public DateTime Version { get; init; }
        public bool IsDeleted { get; init; }

    }
}
