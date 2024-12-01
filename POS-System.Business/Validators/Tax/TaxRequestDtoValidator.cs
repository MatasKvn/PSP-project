using FluentValidation;
using POS_System.Business.Dtos.Tax;

namespace POS_System.Business.Validators.Tax
{
    public class TaxRequestDtoValidator : AbstractValidator<TaxRequestDto>
    {
        public TaxRequestDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(TaxValidationMessages.TaxNameRequired)
                .Length(TaxValidationConstants.TaxNameMinLength, TaxValidationConstants.TaxNameMaxLength)
                .WithMessage(TaxValidationMessages.TaxNameLengthConstraint);

            RuleFor(x => x.Rate)
                .NotEmpty()
                .WithMessage(TaxValidationMessages.TaxRateRequired)
                .InclusiveBetween(TaxValidationConstants.TaxRatePercentageMinValue, TaxValidationConstants.TaxRatePercentageMaxValue)
                .When(x => x.IsPercentage)
                .WithMessage(TaxValidationMessages.TaxRatePercentageValueConstraint)
                .GreaterThan(TaxValidationConstants.TaxRateFlatMinValue)
                .LessThanOrEqualTo(TaxValidationConstants.TaxRateFlatMaxValue)
                .When(x => !x.IsPercentage)
                .WithMessage(TaxValidationMessages.TaxRateFlatValueConstraint);

            RuleFor(x => x.IsPercentage)
                .NotEmpty()
                .WithMessage(TaxValidationMessages.TaxIsPercentageRequired);
        }
    }
}
