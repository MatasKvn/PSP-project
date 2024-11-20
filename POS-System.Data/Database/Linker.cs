using Microsoft.EntityFrameworkCore;
using POS_System.Data.Identity;
using POS_System.Domain.Entities;

namespace POS_System.Data.Database
{
    public static class Linker
    {
        public static void LinkAll(ModelBuilder modelBuilder)
        {
            LinkApplicationUser(modelBuilder);
        }

        public static void LinkApplicationUser(ModelBuilder modelBuilder)
        {
            //Increment taxID by one on creation if not specified explicitly
            modelBuilder.Entity<Tax>()
                .Property(t => t.TaxId)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(startValue: 1, incrementBy: 1);

            //Manual links for ApplicationUser<int> (new Employee.cs)
            modelBuilder.Entity<ApplicationUser<int>>()
                .HasMany<EmployeeOnService>()
                .WithOne()
                .HasForeignKey(e => e.EmployeeVersionId)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser<int>>()
                .HasMany<TimeSlot>()
                .WithOne()
                .HasForeignKey(e => e.EmployeeVersionId);

            modelBuilder.Entity<ApplicationUser<int>>()
                .HasMany<Cart>()
                .WithOne()
                .HasForeignKey(e => e.EmployeeVersionId);
        }
    }
}
