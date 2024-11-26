﻿using FluentValidation;
using FluentValidation.AspNetCore;
using POS_System.Business.Dtos.GiftCard;

namespace POS_System.Api.Configurations;

public static class ValidatorConfiguration
{
    public static WebApplicationBuilder ConfigureValidators(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder, nameof(builder));

        builder.Services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssemblyContaining<GiftCardRequestDto>();

        return builder;
    }
}
