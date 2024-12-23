namespace POS_System.Common.Constants
{
    public class ApplicationMessages
    {
        public const string ACCOUNT_LOCKED_OUT = "Your account is locked due to too many failed login attempts. Try again after 5 minutes.";
        public const string INVALID_SIGN_IN_CREDS = "Invalid sign in credentials. Wrong username or password.";
        public const string USER_CREATION_FAIL = "A user with this username already exists.";
        public const string INSUFFICIENT_DATA = "Provided data is insufficient.";
        public const string INTERNAL_SERVER_ERROR = "Something went wrong on server side.";
        public const string EMAIL_SENT_INFO = "Email sent successfully.";
        public const string PASSWORD_RESET_MAIL_TITLE = "Password reset for POS-System.";
        public const string INVALID_PASS_RECOVERY_CREDS = "Provided data is invalid.";
        public const string SUCCESSFUL_ACTION = "Action performed successfully.";
        public const string NOT_FOUND_ERROR = "Resource not found.";
        public const string BAD_REQUEST_MESSAGE = "Provided data is incorrect.";
        public const string TAX_NOT_FOUND = "No tax with such id was found!";
        public const string WEB_HOST_NOT_SET = "Web host not set explicitly.";
        public const string EXPIRED_DISCOUNT = "Discount is no longer available.";
        public const string INSUFFICIENT_TOTAL_AMOUNT = "Total price of a cart is too little. Partial payment is not allowed.";
        public const string CLOSED_ORDER = "Order was closed or is in either payment or refund phase.";
        public const string GIFT_CARD_NOT_VALID = "Gift card has expired or does not exist.";
        public const string TRANSACTION_INNACURACY = "Transaction record does not not exist.";
        public const string INVALID_REFUND_REQUEST = "Cannot refund non-completed carts";
        public const string CART_NOT_IN_PROGRESS = "Cart is not in progress";
    }
}