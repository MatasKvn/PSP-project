namespace POS_System.Business.Validators.Tax
{
    public static class TaxValidationMessages
    {
        public const string TaxNameRequired = "Tax name is required.";
        public static readonly string TaxNameLengthConstraint = $"Tax name length should be between {TaxValidationConstants.TaxNameMinLength} and {TaxValidationConstants.TaxNameMaxLength} characters.";

        public const string TaxRateRequired = "Tax rate is required.";
        public static readonly string TaxRatePercentageValueConstraint = $"Percentage tax value should be between {TaxValidationConstants.TaxRatePercentageMinValue} and {TaxValidationConstants.TaxRatePercentageMaxValue}.";
        public static readonly string TaxRateFlatValueConstraint = $"Flat tax value should be between {TaxValidationConstants.TaxRateFlatMinValue} and {TaxValidationConstants.TaxRateFlatMaxValue}.";

        public const string TaxIsPercentageRequired = "Tax isPercentage is required.";
    }
}
