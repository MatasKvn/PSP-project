namespace POS_System.Business.Dtos.Request
{
    public record InitPartialCheckoutRequest(
        int CartId, 
        int EmployeeId,
        int PaymentCount, 
        int? Tip, 
        List<CheckoutCartItem> CartItems
    );
}
