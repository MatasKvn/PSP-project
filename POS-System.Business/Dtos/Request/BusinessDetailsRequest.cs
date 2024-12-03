namespace POS_System.Business.Dtos.Request
{
    public record BusinessDetailsRequest
    {
        public required string BusinessName { get; set; }
        public required string BusinessEmail { get; set; }
        public required string BusinessPhone { get; set; }
        public required string Country { get; set; }
        public required string City { get; set; }
        public required string Street { get; set; }
        public required int HouseNumber { get; set; }
        public required int? FlatNumber { get; set; }
    }
}
