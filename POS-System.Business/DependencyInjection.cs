using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using POS_System.Business.AutoMapper;
using POS_System.Business.Dtos;
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
            var emailConfig = configuration
                .GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();

            services.AddSingleton(emailConfig!);
            services.AddScoped<IEmailSender, EmailSender>();

            // Register Business layer services
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
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

            services.AddAuthorization(options => 
            {
                options.AddPolicy("TransactionWrite", policy => policy.RequireClaim("TransactionWrite"));
                options.AddPolicy("TransactionRead", policy => policy.RequireClaim("TransactionRead"));
                options.AddPolicy("HistoricTransactionWrite", policy => policy.RequireClaim("HistoricTransactionWrite"));
                options.AddPolicy("HistoricTransactionRead", policy => policy.RequireClaim("HistoricTransactionRead"));
                options.AddPolicy("ServiceWrite", policy => policy.RequireClaim("ServiceWrite"));
                options.AddPolicy("ServiceRead", policy => policy.RequireClaim("ServiceRead"));
                options.AddPolicy("ItemWrite", policy => policy.RequireClaim("ItemWrite"));
                options.AddPolicy("ItemRead", policy => policy.RequireClaim("ItemRead"));
                options.AddPolicy("EmployeesWrite", policy => policy.RequireClaim("EmployeesWrite"));
                options.AddPolicy("EmployeesRead", policy => policy.RequireClaim("EmployeesRead"));
                options.AddPolicy("TaxWrite", policy => policy.RequireClaim("TaxWrite"));
                options.AddPolicy("TaxRead", policy => policy.RequireClaim("TaxRead"));
                options.AddPolicy("HistoricWrite", policy => policy.RequireClaim("HistoricWrite"));
                options.AddPolicy("HistoricRead", policy => policy.RequireClaim("HistoricRead"));
            });

            services.AddScoped<ITaxService, TaxService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IProductModificationService, ProductModificationService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ITimeSlotService, TimeSlotService>();
            services.AddScoped<IProductOnTaxService, ProductOnTaxService>();
            services.AddScoped<IServiceOnTaxService, ServiceOnTaxService>();
            services.AddScoped<IGiftCardService, GiftCardService>();

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