using Microsoft.Extensions.Configuration;
using POS_System.Business.AutoMapper;
using POS_System.Business.Services;
using POS_System.Business.Services.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Business layer services
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ITaxService, TaxService>();
            services.AddScoped<IProductModificationService, ProductModificationService>();

            return services;
        }
    }
}