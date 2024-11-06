using Microsoft.EntityFrameworkCore;
using POS_System.Domain.Entities;

namespace POS_System.Data.Database
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Tax> Taxes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tax seeding
            var tax1 = new Tax(1, "Test", 5, DateTime.Now, true, false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
