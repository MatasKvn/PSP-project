namespace POS_System.Business.Dtos.Response
{
    public record TaxResponse
    {
        public int Id { get; set; }
        public int TaxId { get; set; }
        public required string Name { get; set; }
        public required int Rate { get; set; }
        public required bool IsPercentage { get; set; }
        public DateTime Version { get; set; }
        public bool IsDeleted { get; set; }
    }
}
