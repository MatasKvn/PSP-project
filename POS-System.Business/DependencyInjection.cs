using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using POS_System.Business.AutoMapper;
using POS_System.Business.Services;
using POS_System.Business.Services.Interfaces;
using POS_System.Business.Utils;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {
            var secretKey = TryGetConfigValue(configuration, "POSJwtSecretKey");

            // Register Business layer services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = TryGetConfigValue(configuration, "POSIssuer"),
                    ValidAudience = TryGetConfigValue(configuration, "POSAudience"),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
                };
            });

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            services.AddScoped<ITaxService, TaxService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

        public static string? TryGetConfigValue(IConfiguration configuration, string key)
        {
            var value = configuration[key];

            if (value is null)
            {
                Console.WriteLine($"[ERROR] Missing configuration value for key '{key}'");
                Environment.Exit(1);
            }

            return value;
        }
    }
}