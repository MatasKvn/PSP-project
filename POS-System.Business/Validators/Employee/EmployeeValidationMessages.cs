namespace POS_System.Business.Validators.CartItem
{
    public static class EmployeeValidationMessages
    {
        public const string UserNameRequired = "User name is required.";
        public const string InvalidPhoneNumber = "Phone number can start with (+) and can contain only numbers (max length 14, no spaces).";
        public const string AgeRestriction = "You must be at least 16 years old.";
    }
}