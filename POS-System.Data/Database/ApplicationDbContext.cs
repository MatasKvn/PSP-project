using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using POS_System.Data.Identity;
using POS_System.Domain.Entities;

namespace POS_System.Data.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : IdentityDbContext<ApplicationUser, ApplicationRole, int>(options)
    {
        //DbSets
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDiscount> CardDiscounts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<EmployeeOnService> EmployeeOnServices { get; set; }
        public DbSet<GiftCard> GiftCards { get; set; }
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
        public DbSet<ApplicationUser> Employees { get; set; }

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
