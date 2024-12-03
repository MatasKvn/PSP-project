using FluentValidation;
using FluentValidation.AspNetCore;
using POS_System.Business.Dtos.BusinessDetails;
using POS_System.Business.Dtos.GiftCard;
using POS_System.Business.Dtos.Tax;

namespace POS_System.Api.Configurations;

public static class ValidatorConfiguration
{
    public static WebApplicationBuilder ConfigureValidators(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.Services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining<GiftCardRequestDto>()
            .AddValidatorsFromAssemblyContaining<TaxRequestDto>()
            .AddValidatorsFromAssemblyContaining<BusinessDetailsRequestDto>();

        return builder;
    }
}
