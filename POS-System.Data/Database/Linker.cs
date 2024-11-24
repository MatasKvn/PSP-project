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
        }
    }
}
