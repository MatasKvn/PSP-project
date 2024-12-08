using FluentValidation;
using POS_System.Business.Dtos.Request;

namespace POS_System.Business.Validators.Service
{
    public class ServiceRequestValidator : AbstractValidator<ServiceRequest>
    {
        public ServiceRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ServiceValidationMessages.NameRequired)
                .MaximumLength(ServiceValidationConstants.NameMaxLength)
                .WithMessage(ServiceValidationMessages.NameTooLong);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage(ServiceValidationMessages.DescriptionRequired)
                .MaximumLength(ServiceValidationConstants.DescriptionMaxLength)
                .WithMessage(ServiceValidationMessages.DescriptionTooLong);

            RuleFor(x => x.Duration)
                .GreaterThanOrEqualTo(ServiceValidationConstants.MinDuration)
                .WithMessage(ServiceValidationMessages.DurationInvalid);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(ServiceValidationConstants.MinPrice)
                .WithMessage(ServiceValidationMessages.PriceInvalid);

            RuleFor(x => x.ImageURL)
                .NotEmpty()
                .WithMessage(ServiceValidationMessages.ImageURLRequired)
                .Must(IsValidUrl)
                .WithMessage(ServiceValidationMessages.ImageURLInvalid);
        }

        private bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
