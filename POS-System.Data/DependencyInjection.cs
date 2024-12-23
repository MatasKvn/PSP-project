using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using POS_System.Data.Database;
using POS_System.Data.Identity;
using POS_System.Data.Repositories;
using POS_System.Data.Repositories.Base;
using POS_System.Data.Repositories.Interfaces;
using POS_System.Domain.Entities;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") ?? configuration.GetConnectionString("LocalConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString, npgsqlOptions => npgsqlOptions.MigrationsAssembly("POS-System.Data")));
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var builder = services.AddIdentityCore<ApplicationUser>(options => 
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
                options.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(ApplicationRole), builder.Services);
            builder
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<ICartDiscountRepository, CartDiscountRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<ICartItemRepository, CartItemRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IGiftCardRepository, GiftCardRepository>();
            services.AddScoped<IItemDiscountRepository, ItemDiscountRepository>();
            services.AddScoped<IProductModificationRepository, ProductModificationRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IServiceReservationRepository, ServiceReservationRepository>();
            services.AddScoped<ITaxRepository, TaxRepository>();
            services.AddScoped<ITimeSlotRepository, TimeSlotRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IBusinessDetailRepository, BusinessDetailRepository>();
            services.AddScoped<IGenericManyToManyRepository<Product, Tax, ProductOnTax>, GenericManyToManyRepository<Product, Tax, ProductOnTax>>();
            services.AddScoped<IGenericManyToManyRepository<Service, Tax, ServiceOnTax>, GenericManyToManyRepository<Service, Tax, ServiceOnTax>>();
            services.AddScoped<IGenericManyToManyRepository<ProductModification, CartItem, ProductModificationOnCartItem>,
                GenericManyToManyRepository<ProductModification, CartItem, ProductModificationOnCartItem>>();
            services.AddScoped<IGenericManyToManyRepository<Product, ItemDiscount, ProductOnItemDiscount>,
                GenericManyToManyRepository<Product, ItemDiscount, ProductOnItemDiscount>>();
            services.AddScoped<IGenericManyToManyRepository<Service, ItemDiscount, ServiceOnItemDiscount>,
                GenericManyToManyRepository<Service, ItemDiscount, ServiceOnItemDiscount>>();
            services.AddScoped<IGenericManyToManyRepository<ProductModification, CartItem, ProductModificationOnCartItem>,
                GenericManyToManyRepository<ProductModification, CartItem, ProductModificationOnCartItem>>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        
            return services;
        }
    }
}