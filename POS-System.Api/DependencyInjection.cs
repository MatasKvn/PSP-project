using POS_System.Business.Services;
using POS_System.Business.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Api layer services
            services.AddControllers();

            return services;
        }
    }
}