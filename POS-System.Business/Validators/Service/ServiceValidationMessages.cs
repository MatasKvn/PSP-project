using POS_System.Business.Validators.GiftCard;

namespace POS_System.Business.Validators.Service
{
    public static class ServiceValidationMessages
    {
        public const string NameRequired = "Service name is required.";
        public static readonly string NameTooLong = $"Service name cannot exceed {ServiceValidationConstants.NameMaxLength} characters.";
        public const string DescriptionRequired = "Service description is required.";
        public static readonly string DescriptionTooLong = $"Description cannot exceed {ServiceValidationConstants.DescriptionMaxLength} characters.";
        public const string DurationInvalid = "Duration must be greater than 0.";
        public const string PriceInvalid = "Price must be a non-negative value.";
        public const string ImageURLRequired = "Image URL is required.";
        public const string ImageURLInvalid = "Image URL must be a valid URL.";
    }
}