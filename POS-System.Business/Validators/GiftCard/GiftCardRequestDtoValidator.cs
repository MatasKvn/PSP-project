using FluentValidation;
using POS_System.Business.Dtos.Request;

namespace POS_System.Business.Validators.GiftCard
{
    public class GiftCardRequestDtoValidator : AbstractValidator<GiftCardRequest>
    {
        public GiftCardRequestDtoValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage(GiftCardValidationMessages.ExpirationDateRequired)
                .Must(date => date.CompareTo(DateOnly.FromDateTime(DateTime.Now)) >= 0)
                .WithMessage(GiftCardValidationMessages.ExpirationDateInPast);

            RuleFor(x => x.Value)
                .GreaterThan(0)
                .WithMessage(GiftCardValidationMessages.ValueMustBePositive);

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage(GiftCardValidationMessages.CodeRequired)
                .Length(GiftCardValidationConstants.CodeMinLength, GiftCardValidationConstants.CodeMaxLength)
                .WithMessage(GiftCardValidationMessages.CodeLengthInvalid);
        }
    }
}