namespace POS_System.Business.Validators.GiftCard;

public static class GiftCardValidationMessages
{
    public const string ExpirationDateRequired = "Expiration date is required.";
    public const string ExpirationDateInPast = "Expiration date cannot be in the past.";
    public const string ValueMustBePositive = "Value must be greater than 0.";
    public const string CodeRequired = "Code is required.";
    public static readonly string CodeLengthInvalid = $"Code must be between {GiftCardValidationConstants.CodeMinLength} and {GiftCardValidationConstants.CodeMaxLength} characters.";
}