using FluentValidation;
using POS_System.Business.Dtos.Request;

namespace POS_System.Business.Validators.ProductModification
{
    public class ProductModificationRequestValidator : AbstractValidator<ProductModificationRequest>
    {
        public ProductModificationRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name for product modification is required.")
                .MaximumLength(40)
                .WithMessage("Name for product modification cannot exceed 40 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description for product modification is required.")
                .MaximumLength(255)
                .WithMessage("Description for product modification cannot exceed 255 characters.");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price value for product modification is required.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price value for product modification must be greater or equal to 0.");
        }
    }
}
