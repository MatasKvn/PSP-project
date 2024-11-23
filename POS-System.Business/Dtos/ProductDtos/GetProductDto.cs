using System.ComponentModel.DataAnnotations;

namespace POS_System.Business.Dtos.ProductDtos
{
    public record GetProductDto
    {
        //Primary key
        [Key]
        public int Id { get; set; }

        //Fields
        public int ProductId { get; set; }
        [MaxLength(40)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }
        public required string ImageURL { get; set; }
        public required int Stock { get; set; }

        //Versioning
        public DateTime Version { get; set; }
        public bool IsDeleted { get; set; }

    }
}
