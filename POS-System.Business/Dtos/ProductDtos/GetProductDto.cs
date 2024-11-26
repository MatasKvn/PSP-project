namespace POS_System.Business.Dtos.ProductDtos
{
    public record GetProductDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }
        public required string ImageURL { get; set; }
        public required int Stock { get; set; }
        public DateTime Version { get; set; }
        public bool IsDeleted { get; set; }

    }
}
