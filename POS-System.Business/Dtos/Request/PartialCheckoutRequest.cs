namespace POS_System.Business.Dtos.Request
{
    public record PartialCheckoutRequest(
        int CartId,
        DateTime Id,
        GiftCardDetails? GiftCard
    );
}