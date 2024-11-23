using System.ComponentModel.DataAnnotations;

namespace POS_System.Business.Dtos.ProductDtos
{
    public record CreateProductDto
    {
        [MaxLength(40)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }
        public required string ImageURL { get; set; }
        public required int Stock { get; set; }
    }
}
