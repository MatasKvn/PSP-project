namespace POS_System.Business.Dtos.Request
{
    public class TaxRequest
    {
        public required string Name { get; set; }
        public required int Rate { get; set; }
        public required bool IsPercentage { get; set; }
    }
}
