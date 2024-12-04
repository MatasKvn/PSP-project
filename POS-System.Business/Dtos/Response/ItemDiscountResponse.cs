namespace POS_System.Business.Dtos.Response
{
    public record ItemDiscountResponse
    {
        public int Id { get; set; }
        public required int ItemDiscountId { get; set; }
        public required int Value { get; set; }
        public required bool IsPercentage { get; set; }
        public required string Description { get; set; }
        public required DateTime? StartDate { get; set; }
        public required DateTime? EndDate { get; set; }
    }
}
