namespace POS_System.Business.Dtos.Request
{
    public record CartItemRequest
    {
        public required int CartId { get; set; }
        public required int Quantity { get; set; }
        public required bool IsProduct { get; set; }

        public int? ProductVersionId { get; set; }
        public int? ServiceVersionId { get; set; }
    }
}