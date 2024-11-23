using Microsoft.EntityFrameworkCore;
using POS_System.Data.Identity;
using POS_System.Domain.Entities;

namespace POS_System.Data.Database
{
    public static class Seeder
    {
        public static void SeedAll(ModelBuilder modelBuilder)
        {
            SeedEmployees(modelBuilder);
            SeedTimeSlots(modelBuilder);
        }

        public static void SeedEmployees(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser<int>>().HasData(
                new ApplicationUser<int> { Id = 1, EmployeeId = 1, FirstName = "John", LastName = "Doe", UserName = "johndoe", NormalizedUserName = "johndoe", Email = "johndoe@example.com", NormalizedEmail = "johndoe@example.com", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEL7rWl6+6gQmXvT4XvH8z9FV3WzQX1lKoHkxJ7F5oF+U4T5RrH3RrQbV9T8M2Q1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", SecurityStamp = "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", ConcurrencyStamp = "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 },
                new ApplicationUser<int> { Id = 2, EmployeeId = 2, FirstName = "Jane", LastName = "Doe", UserName = "janedoe", NormalizedUserName = "janedoe", Email = "janedoe@example.com", NormalizedEmail = "janedoe@example.com", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEL7rWl6+6gQmXvT4XvH8z9FV3WzQX1lKoHkxJ7F5oF+U4T5RrH3RrQbV9T8M2Q1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", SecurityStamp = "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", ConcurrencyStamp = "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 },
                new ApplicationUser<int> { Id = 3, EmployeeId = 3, FirstName = "Adam", LastName = "Smith", UserName = "adamsmith", NormalizedUserName = "adamsmith", Email = "adamsmith@example.com", NormalizedEmail = "adamsmith@example.com", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEL7rWl6+6gQmXvT4XvH8z9FV3WzQX1lKoHkxJ7F5oF+U4T5RrH3RrQbV9T8M2Q1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", SecurityStamp = "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", ConcurrencyStamp = "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 },
                new ApplicationUser<int> { Id = 4, EmployeeId = 4, FirstName = "Bob", LastName = "Johnson", UserName = "bobjohnson", NormalizedUserName = "bobjohnson", Email = "bobjohnson@example.com", NormalizedEmail = "bobjohnson@example.com", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEL7rWl6+6gQmXvT4XvH8z9FV3WzQX1lKoHkxJ7F5oF+U4T5RrH3RrQbV9T8M2Q1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", SecurityStamp = "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", ConcurrencyStamp = "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 },
                new ApplicationUser<int> { Id = 5, EmployeeId = 1, FirstName = "Johnson", LastName = "Doe", UserName = "johnsondoe", NormalizedUserName = "johnsondoe", Email = "johndoe@example.com", NormalizedEmail = "johndoe@example.com", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEL7rWl6+6gQmXvT4XvH8z9FV3WzQX1lKoHkxJ7F5oF+U4T5RrH3RrQbV9T8M2Q1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", SecurityStamp = "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", ConcurrencyStamp = "N3J7G6F5D4C3B2A1O0N3P2L1K0J9I8H7G6F5D4C3B2A1", PhoneNumberConfirmed = true, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 }
            );
        }

        public static void SeedTimeSlots(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeSlot>().HasData(
                new TimeSlot { Id = 1, EmployeeVersionId = 1, StartTime = DateTime.Now, IsAvailable = true},
                new TimeSlot { Id = 2, EmployeeVersionId = 1, StartTime = DateTime.Now, IsAvailable = true },
                new TimeSlot { Id = 3, EmployeeVersionId = 2, StartTime = DateTime.Now, IsAvailable = false },
                new TimeSlot { Id = 4, EmployeeVersionId = 3, StartTime = DateTime.Now, IsAvailable = true }
            );
        }
    }
}
