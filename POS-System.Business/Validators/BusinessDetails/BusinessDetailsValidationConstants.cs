using System.Security.Policy;

namespace POS_System.Business.Validators.BusinessDetails
{
    public static class BusinessDetailsValidationConstants
    {
        public const int BusinessNameMinLength = 1;
        public const int BusinessNameMaxLength = 50;

        public const int BusinessCountryMinLength = 1;
        public const int BusinessCountryMaxLength = 255;

        public const int BusinessCityMinLength = 1;
        public const int BusinessCityMaxLength = 255;

        public const int BusinessStreetMinLength = 1;
        public const int BusinessStreetMaxLength = 255;

        public const int BusinessHouseNumberMinValue = 1;
        public const int BusinessHouseNumberMaxValue = int.MaxValue;

        public const int BusinessFlatNumberMinValue = 1;
        public const int BusinessFlatNumberMaxValue = int.MaxValue;
    }
}
