using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS_System.Data.Identity;
using POS_System.Domain.Entities;

namespace POS_System.Data.Database
{
    public class ApplicationDbContext<TKey>(DbContextOptions<ApplicationDbContext<TKey>> options) 
        : IdentityDbContext<ApplicationUser<TKey>, IdentityRole<TKey>, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>, 
          IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>(options)
            where TKey : IEquatable<TKey>
    {
        public DbSet<CardDetails> CardDetails { get; set; }
        public DbSet<CartCartDiscount> Carts { get; set; }
        public DbSet<Cart> CartCartDisocunts { get; set; }
        public DbSet<CartDiscount> CardDiscounts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<EmployeeService> EmployeeServices { get; set; }
        public DbSet<GiftCard> GiftCards { get; set; }
        public DbSet<GiftCardDetails> GiftCardDetails { get; set; }
        public DbSet<ItemDiscount> ItemDiscounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItemDiscount> ProductItemDiscounts { get; set; }
        public DbSet<ProductModification> ProductModifications { get; set; }
        public DbSet<ProductModificationCartItem> ProductModificationCartItems { get; set; }
        public DbSet<ProductTax> ProductTaxes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceItemDiscount> ServiceItemDiscounts { get; set; }
        public DbSet<ServiceReservation> ServiceReservations { get; set; }
        public DbSet<ServiceTax> ServiceTaxes { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // DatabaseSeeding();
            DatabaseLinking(modelBuilder);

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }

        private void DatabaseSeeding()
        {
            //Tax seeding
            var tax1 = new Tax
            {
                Id = 1,
                Name = "Test",
                Rate = 5,
                IsPercentage = true,
                Version = DateTime.Now,
                IsDeleted = false
            };
        }

        private void DatabaseLinking(ModelBuilder modelBuilder)
        {
            // GiftCard -> GiftCardDetails
            modelBuilder.Entity<GiftCard>()
                .HasMany(e => e.GiftCardDetails)
                .WithOne(e => e.GiftCard)
                .HasForeignKey(e => e.GiftCardId)
                .IsRequired();

            // Service -> ServiceTax <- Tax
            modelBuilder.Entity<Service>()
                .HasMany(e => e.Taxes)
                .WithMany(e => e.Services)
                .UsingEntity<ServiceTax>();

            // Product -> ProductTax <- Tax
            modelBuilder.Entity<Product>()
                .HasMany(e => e.Taxes)
                .WithMany(e => e.Products)
                .UsingEntity<ProductTax>();

            modelBuilder.Entity<ApplicationUser<int>>()
                .HasMany<EmployeeService>()
                .WithOne()
                .HasForeignKey(e => e.EmployeeId)
                .IsRequired();     

            modelBuilder.Entity<ApplicationUser<int>>()
                .HasMany<TimeSlot>()
                .WithOne()
                .HasForeignKey(e => e.EmployeeId);     
        }
    }
}
