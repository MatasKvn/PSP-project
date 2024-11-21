using Microsoft.OpenApi.Models;

namespace POS_System.Api.Configuration;

public static class SwaggerConfiguration
{
    public static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "POS-System API", Version = "v1" });
        });

        return builder;
    }
}