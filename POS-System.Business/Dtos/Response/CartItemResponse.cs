namespace POS_System.Business.Dtos.Response
{
    public record class CartItemResponse
    {
        public int Id { get; init; }
        public int CartId { get; set; }
        public required int Quantity { get; set; }
        public required bool IsProduct { get; set; }

        public int? ProductVersionId { get; set; }
        public int? ServiceVersionId { get; set; }
    }
}