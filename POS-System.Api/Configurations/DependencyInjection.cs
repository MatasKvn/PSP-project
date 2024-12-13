using POS_System.Api.ExceptionHandler;

namespace POS_System.Api.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register Api layer services
        services.AddControllers();

        return services;
    }

    public static WebApplicationBuilder AddGlobalExceptionHandler(this WebApplicationBuilder builder)
    {
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        return builder;
    }
}