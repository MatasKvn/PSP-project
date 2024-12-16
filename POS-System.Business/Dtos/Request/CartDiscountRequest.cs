namespace POS_System.Business.Dtos.Request
{
    public record CartDiscountRequest
    {
        public required int Value { get; set; }
        public required bool IsPercentage { get; set; }
        public required DateTime? EndDate { get; set; }
    }
}
