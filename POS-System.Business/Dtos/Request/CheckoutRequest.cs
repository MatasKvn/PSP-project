namespace POS_System.Business.Dtos.Request
{
    public record CheckoutRequest(
        int CartId, 
        int EmployeeId,
        int? Tip,
        string? PhoneNumber,
        List<CheckoutCartItem> CartItems
    );
}
