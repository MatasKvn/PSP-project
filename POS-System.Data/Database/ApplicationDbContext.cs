using Microsoft.EntityFrameworkCore;
using POS_System.Domain.Entities;


namespace POS_System.Data.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<CardDetails> CardDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Cart> CartCartDisocunts { get; set; }
        public DbSet<CartDiscount> CardDiscounts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeService> EmployeeServices { get; set; }
        public DbSet<GiftCard> GiftCards { get; set; }
        public DbSet<GiftCardDetails> GiftCardDetails { get; set; }
        public DbSet<ItemDiscount> ItemDiscounts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Product> ProductItemDiscounts { get; set; }
        public DbSet<ProductModification> ProductModifications { get; set; }
        public DbSet<ProductModificationCartItem> ProductModificationCartItems { get; set; }
        public DbSet<ProductTax> ProductTaxes { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Product> ServiceItemDiscounts { get; set; }
        public DbSet<ServiceReservation> ServiceReservations { get; set; }
        public DbSet<ServiceTax> ServiceTaxes { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
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

        }
    }
}
