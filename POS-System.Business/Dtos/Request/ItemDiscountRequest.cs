namespace POS_System.Business.Dtos.Request
{
    public record ItemDiscountRequest
    {
        public required int Value { get; set; }
        public required bool IsPercentage { get; set; }
        public required string Description { get; set; }
        public required DateTime? StartDate { get; set; }
        public required DateTime? EndDate { get; set; }
    }
}
