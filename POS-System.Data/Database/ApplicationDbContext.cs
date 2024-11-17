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
        //DbSets
        public DbSet<CardDetails> CardDetails { get; set; }
        public DbSet<CartOnCartDiscount> Carts { get; set; }
        public DbSet<Cart> CartOnCartDisocunts { get; set; }
        public DbSet<CartDiscount> CardDiscounts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<EmployeeOnService> EmployeeOnServices { get; set; }
        public DbSet<GiftCard> GiftCards { get; set; }
        public DbSet<GiftCardDetails> GiftCardDetails { get; set; }
        public DbSet<ItemDiscount> ItemDiscounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOnItemDiscount> ProductOnItemDiscounts { get; set; }
        public DbSet<ProductModification> ProductModifications { get; set; }
        public DbSet<ProductModificationOnCartItem> ProductModificationOnCartItems { get; set; }
        public DbSet<ProductOnTax> ProductOnTaxes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceOnItemDiscount> ServiceOnItemDiscounts { get; set; }
        public DbSet<ServiceReservation> ServiceReservations { get; set; }
        public DbSet<ServiceOnTax> ServiceOnTaxes { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        //Functions
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Linker.LinkAll(modelBuilder);
            Seeder.SeedAll(modelBuilder);

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }
    }
}
