namespace POS_System.Business.Dtos.Request
{
    public record ProductModificationRequest
    {
        public int ProductVersionId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }
    }
}
