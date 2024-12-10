namespace POS_System.Business.Validators.CartItem
{
    public static class CartItemValidationMessages
    {
        public const string CartIdRequired = "Cart ID is required.";
        public const string QuantityInvalid = "Quantity must be greater than or equal to 1.";
        public const string ItemTypeMismatch = "A cart item must be either a product or a service.";
    }
}