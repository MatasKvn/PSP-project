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
            modelBuilder.Entity<ProductModification>()
                .Property(t => t.ProductModificationId)
                .UseIdentityColumn()
                .HasIdentityOptions(startValue: 1, incrementBy: 1);

            modelBuilder.Entity<Product>()
                .Property(t => t.ProductId)
                .UseIdentityColumn()
                .HasIdentityOptions(startValue: 1, incrementBy: 1);

            //Increment taxID by one on creation if not specified explicitly
            modelBuilder.Entity<Tax>()
                .Property(t => t.TaxId)
                .UseIdentityColumn()
                .HasIdentityOptions(startValue: 1, incrementBy: 1);

            modelBuilder.Entity<ItemDiscount>()
                .Property(t => t.ItemDiscountId)
                .UseIdentityColumn()
                .HasIdentityOptions(startValue: 1, incrementBy: 1);

            //Manual links for ApplicationUser (new Employee.cs)
            modelBuilder.Entity<ApplicationUser>()
                .HasMany<EmployeeOnService>()
                .WithOne()
                .HasForeignKey(e => e.EmployeeVersionId)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany<TimeSlot>()
                .WithOne()
                .HasForeignKey(e => e.EmployeeVersionId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany<Cart>()
                .WithOne()
                .HasForeignKey(e => e.EmployeeVersionId);

            modelBuilder.Entity<Service>()
               .Property(t => t.ServiceId)
               .UseIdentityColumn()
               .HasIdentityOptions(startValue: 1, incrementBy: 1);
        }
    }
}
