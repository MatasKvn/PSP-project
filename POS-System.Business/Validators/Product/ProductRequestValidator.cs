using FluentValidation;
using POS_System.Business.Dtos.Request;

namespace POS_System.Business.Validators.Product
{
    public class ProductRequestValidator : AbstractValidator<ProductRequest>
    {
        public ProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Product name is required.")
                .MaximumLength(40)
                .WithMessage("Product name cannot exceed 40 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Product description is required.")
                .MaximumLength(255)
                .WithMessage("Product description cannot exceed 255 characters.");

            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Product price value is required.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product price must be equal or greater than 0.");

            RuleFor(x => x.Stock)
                .NotEmpty()
                .WithMessage("Product stock value is required.")
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product stock must be equal or greater than 0.");

            RuleFor(x => x.ImageURL)
                .NotEmpty()
                .WithMessage("An image URL for product must be provided.")
                .Must(IsValidUrl)
                .WithMessage("Image URL must be a valid url.");
        }

        private bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
