using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using POS_System.Data.Database;
using POS_System.Data.Identity;
using POS_System.Data.Repositories;
using POS_System.Data.Repositories.Base;
using POS_System.Data.Repositories.Interfaces;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
            //var connectionString = configuration.GetConnectionString("LocalConnection");
            services.AddDbContext<ApplicationDbContext<int>>(options =>
                options.UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.MigrationsAssembly("POS-System.Data")));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services
                .AddIdentity<ApplicationUser<int>, IdentityRole<int>>()
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext<int>>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ICardDetailsRepository, CardDetailsRepository>();
            services.AddScoped<ICartDiscountRepository, CartDiscountRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IGiftCardDetailsRepository, GiftCardDetailsRepository>();
            services.AddScoped<IGiftCardRepository, GiftCardRepository>();
            services.AddScoped<IItemDiscountRepository, ItemDiscountRepository>();
            services.AddScoped<IProductModificationRepository, ProductModificationRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IServiceReservationRepository, ServiceReservationRepository>();
            services.AddScoped<ITaxRepository, TaxRepository>();
            services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}