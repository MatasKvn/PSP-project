using FluentValidation;
using POS_System.Business.Dtos.Request;
using POS_System.Business.Validators.Tax;

namespace POS_System.Business.Validators.ItemDiscount
{
    public class ItemDiscountRequestValidator : AbstractValidator<ItemDiscountRequest>
    {
        public ItemDiscountRequestValidator()
        {
            RuleFor(x => x.IsPercentage)
                .NotNull()
                .WithMessage("IsPercentage is required");

            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Item discount value is required.")
                .InclusiveBetween(1, 100)
                .When(x => x.IsPercentage)
                .WithMessage("Item discount percentage value must be between 1 and 100")
                .GreaterThan(0)
                .LessThanOrEqualTo(int.MaxValue)
                .When(x => !x.IsPercentage)
                .WithMessage("Item discount flat value must be more than 0");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x)
                .Must(x => (x.StartDate.HasValue && x.EndDate.HasValue) || (!x.StartDate.HasValue && !x.EndDate.HasValue))
                .WithMessage("Either both StartDate and EndDate must be provided, or both must be null.");

            RuleFor(x => x.StartDate)
                .GreaterThanOrEqualTo(DateTime.UtcNow)
                .When(x => x.StartDate.HasValue)
                .WithMessage("StartDate cannot be in the past.");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("EndDate must be after StartDate.");
        }
    }
}
