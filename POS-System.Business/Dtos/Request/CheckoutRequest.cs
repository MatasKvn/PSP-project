namespace POS_System.Business.Dtos.Request
{
    public record CheckoutRequest(
        int CartId, 
        int EmployeeId, 
        int? Tip, 
        List<CheckoutCartItem> CartItems
    );
}
