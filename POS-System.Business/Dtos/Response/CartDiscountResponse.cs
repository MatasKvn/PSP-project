namespace POS_System.Business.Dtos.Response
{
    public record CartDiscountResponse
    {
        public required string Id { get; set; }
        public required int Value { get; set; }
        public required bool IsPercentage { get; set; }
    }
}
