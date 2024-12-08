namespace POS_System.Business.Dtos.Response
{
    public record CartDiscountResponse
    {
        public int Id { get; set; }
        public int CartDiscountId { get; set; }
        public required int Value { get; set; }
        public required bool IsPercentage { get; set; }
        public required string Description { get; set; }
        public required DateTime? StartDate { get; set; }
        public required DateTime? EndDate { get; set; }
    }
}
