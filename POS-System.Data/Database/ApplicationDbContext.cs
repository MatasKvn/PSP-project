using Microsoft.EntityFrameworkCore;
using POS_System.Domain.Entities;
using System.Reflection.Metadata;

namespace POS_System.Data.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartDiscount> CardDiscounts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeService> EmployeeServices { get; set; }
        public DbSet<ItemDiscount> ItemDiscounts { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTax> ProductTaxes { get; set; }
        public DbSet<ProductModification> ProductModifications { get; set; }
        public DbSet<ProductModificationCartItem> ProductModificationCartItems { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceReservation> ServiceReservations { get; set; }
        public DbSet<ServiceTax> ServiceTaxes { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<CardDetails> CardDetails { get; set; }
        public DbSet<GiftCard> GiftCards { get; set; }
        public DbSet<GiftCardDetails> GiftCardDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DatabaseSeeding();
            DatabaseLinking(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void DatabaseSeeding()
        {
            //Tax seeding
            var tax1 = new Tax(1, "Test", 5, DateTime.Now, true, false);
        }

        private void DatabaseLinking(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<CartDiscount>()
                .HasKey(e => new { e.Id, e.Version });

            modelBuilder.Entity<CartItem>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Employee>()
                .HasKey(e => new { e.Id, e.Version });

            modelBuilder.Entity<EmployeeService>()
                .HasKey(e => new { e.EmployeeId, e.EmployeeVersion, e.ServiceId, e.ServiceVersion });

            modelBuilder.Entity<ItemDiscount>()
                .HasKey(e => new { e.Id, e.Version });

            modelBuilder.Entity<Tax>()
                .HasKey(e => new { e.Id, e.Version });

            modelBuilder.Entity<Product>()
                .HasKey(e => new { e.Id, e.Version });

            modelBuilder.Entity<ProductTax>()
                .HasKey(e => new { e.ProductId, e.ProductVersion, e.TaxId, e.TaxVersion });

            modelBuilder.Entity<ProductModification>()
                .HasKey(e => new { e.Id, e.Version });

            modelBuilder.Entity<ProductModificationCartItem>()
                .HasKey(e => new { e.CartItemId, e.CartItemVersion, e.ProductModificationId, e.ProductModificationVersion});

            modelBuilder.Entity<Service>()
                .HasKey(e => new { e.Id, e.Version });

            modelBuilder.Entity<ServiceReservation>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<ServiceTax>()
                .HasKey(e => new { e.ServiceId, e.ServiceVersion, e.TaxId, e.TaxVersion });

            modelBuilder.Entity<TimeSlot>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<CardDetails>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<GiftCard>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<GiftCardDetails>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Transaction>()
                .HasKey(e => e.Id);
        }
    }
}
