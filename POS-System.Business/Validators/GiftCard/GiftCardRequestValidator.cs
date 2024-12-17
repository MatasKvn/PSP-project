using FluentValidation;
using POS_System.Business.Dtos.Request;

namespace POS_System.Business.Validators.GiftCard
{
    public class GiftCardRequestValidator : AbstractValidator<GiftCardRequest>
    {
        public GiftCardRequestValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage(GiftCardValidationMessages.ExpirationDateRequired)
                .Must(date => date.CompareTo(DateOnly.FromDateTime(DateTime.UtcNow)) >= 0)
                .WithMessage(GiftCardValidationMessages.ExpirationDateInPast);

            RuleFor(x => x.Value)
                .GreaterThan(0)
                .WithMessage(GiftCardValidationMessages.ValueMustBePositive);
        }
    }
}