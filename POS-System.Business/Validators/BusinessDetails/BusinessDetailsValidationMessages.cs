using POS_System.Business.Validators.Tax;

namespace POS_System.Business.Validators.BusinessDetails
{
    public static class BusinessDetailsValidationMessages
    {
        public const string BusinessNameRequired = "Business name is required!";
        public static readonly string BusinessNameLengthConstraint = $"Business name length should be between {BusinessDetailsValidationConstants.BusinessNameMinLength} and {BusinessDetailsValidationConstants.BusinessNameMaxLength} characters.";

        public const string BusinessEmailRequired = "Business email is required!";
        public const string BusinessEmailValidRequired = "A valid business email is required!";

        public const string BusinessPhoneRequired = "Business phone is required!";
        public const string BusinessPhoneInternationalFormatConstraint = "Business phone must be a valid international phone number.";

        public const string BusinessCountryRequired = "Business country is required!";
        public static readonly string BusinessCountryLengthConstraint = $"Business country length should be between {BusinessDetailsValidationConstants.BusinessCountryMinLength} and {BusinessDetailsValidationConstants.BusinessCountryMaxLength} characters.";

        public const string BusinessCityRequired = "Business city is required!";
        public static readonly string BusinessCityLengthConstraint = $"Business city length should be between {BusinessDetailsValidationConstants.BusinessCityMinLength} and {BusinessDetailsValidationConstants.BusinessCityMaxLength} characters.";

        public const string BusinessStreetRequired = "Business street is required!";
        public static readonly string BusinessStreetLengthConstraint = $"Business street length should be between {BusinessDetailsValidationConstants.BusinessStreetMinLength} and {BusinessDetailsValidationConstants.BusinessStreetMaxLength} characters.";

        public const string BusinessHouseNumberRequired = "Business house number is required!";
        public static readonly string BusinessHouseNumberValueConstraint = $"Business house number value should be between {BusinessDetailsValidationConstants.BusinessHouseNumberMinValue} and {BusinessDetailsValidationConstants.BusinessHouseNumberMaxValue}.";

        public static readonly string BusinessFlatNumberValueConstraint = $"Business flat number value should be between {BusinessDetailsValidationConstants.BusinessFlatNumberMinValue} and {BusinessDetailsValidationConstants.BusinessFlatNumberMaxValue}.";
    }
}
