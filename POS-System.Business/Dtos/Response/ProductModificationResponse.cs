namespace POS_System.Business.Dtos.Response
{
    public record ProductModificationResponse
    {
        public int Id { get; set; }
        public int ProductVersionId { get; set; }

        public int ProductModificationId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required int Price { get; set; }
        public DateTime Version { get; set; }
        public bool IsDeleted { get; set; }
    }
}
