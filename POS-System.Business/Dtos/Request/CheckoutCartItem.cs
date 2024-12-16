namespace POS_System.Business.Dtos.Request
{
    public record CheckoutCartItem(
        string Name, 
        string Description, 
        int Price, 
        int Quantity, 
        string ImageURL
    );
}