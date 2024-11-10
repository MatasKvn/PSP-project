using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace POS_System.Business
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Business layer services

            return services;
        }
    }
}