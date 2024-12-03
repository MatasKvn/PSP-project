namespace POS_System.Business.Validators.Tax
{
    public static class TaxValidationConstants
    {
        public const int TaxNameMinLength = 1;
        public const int TaxNameMaxLength = 64;

        public const int TaxRatePercentageMinValue = 1;
        public const int TaxRatePercentageMaxValue = 100;

        public const int TaxRateFlatMinValue = 0;
        public const int TaxRateFlatMaxValue = int.MaxValue;
    }
}
