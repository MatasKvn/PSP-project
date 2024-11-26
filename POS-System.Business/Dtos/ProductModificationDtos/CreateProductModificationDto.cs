namespace POS_System.Business.Dtos.ProductModificationDtos
{
    public record CreateProductModificationDto
    {
        public int ProductVersionId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }
    }
}
