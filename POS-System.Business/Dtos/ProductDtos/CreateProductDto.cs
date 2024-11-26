namespace POS_System.Business.Dtos.ProductDtos
{
    public record CreateProductDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }
        public required string ImageURL { get; set; }
        public required int Stock { get; set; }
    }
}
