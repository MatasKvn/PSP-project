using FluentValidation;
using POS_System.Business.Dtos.Request;

namespace POS_System.Business.Validators.BusinessDetails
{
    public class BusinessDetailsRequestDtoValidator : AbstractValidator<BusinessDetailsRequest>
    {
        public BusinessDetailsRequestDtoValidator()
        {
            RuleFor(x => x.BusinessName)
                .NotEmpty()
                .WithMessage(BusinessDetailsValidationMessages.BusinessNameRequired)
                .Length(BusinessDetailsValidationConstants.BusinessNameMinLength, BusinessDetailsValidationConstants.BusinessNameMaxLength)
                .WithMessage(BusinessDetailsValidationMessages.BusinessNameLengthConstraint);

            RuleFor(x => x.BusinessEmail)
                .NotEmpty()
                .WithMessage(BusinessDetailsValidationMessages.BusinessEmailRequired)
                .EmailAddress()
                .WithMessage(BusinessDetailsValidationMessages.BusinessEmailValidRequired);

            RuleFor(x => x.BusinessPhone)
                .NotEmpty()
                .WithMessage(BusinessDetailsValidationMessages.BusinessPhoneRequired)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage(BusinessDetailsValidationMessages.BusinessPhoneInternationalFormatConstraint);

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage(BusinessDetailsValidationMessages.BusinessCountryRequired)
                .Length(BusinessDetailsValidationConstants.BusinessCountryMinLength, BusinessDetailsValidationConstants.BusinessCountryMaxLength)
                .WithMessage(BusinessDetailsValidationMessages.BusinessCountryLengthConstraint);

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage(BusinessDetailsValidationMessages.BusinessCityRequired)
                .Length(BusinessDetailsValidationConstants.BusinessCityMinLength, BusinessDetailsValidationConstants.BusinessCityMaxLength)
                .WithMessage(BusinessDetailsValidationMessages.BusinessCityLengthConstraint);

            RuleFor(x => x.Street)
                .NotEmpty()
                .WithMessage(BusinessDetailsValidationMessages.BusinessStreetRequired)
                .Length(BusinessDetailsValidationConstants.BusinessStreetMinLength, BusinessDetailsValidationConstants.BusinessStreetMaxLength)
                .WithMessage(BusinessDetailsValidationMessages.BusinessStreetLengthConstraint);

            RuleFor(x => x.HouseNumber)
                .NotEmpty()
                .WithMessage(BusinessDetailsValidationMessages.BusinessHouseNumberRequired)
                .GreaterThan(BusinessDetailsValidationConstants.BusinessHouseNumberMinValue)
                .LessThanOrEqualTo(BusinessDetailsValidationConstants.BusinessHouseNumberMaxValue)
                .WithMessage(BusinessDetailsValidationMessages.BusinessHouseNumberValueConstraint);

            RuleFor(x => x.FlatNumber)
                .GreaterThan(BusinessDetailsValidationConstants.BusinessFlatNumberMinValue)
                .LessThanOrEqualTo(BusinessDetailsValidationConstants.BusinessFlatNumberMaxValue)
                .When(x => x.FlatNumber.HasValue)
                .WithMessage(BusinessDetailsValidationMessages.BusinessFlatNumberValueConstraint);
        }
    }
}
