using Microsoft.EntityFrameworkCore;
using POS_System.Data.Identity;
using POS_System.Domain.Entities;

namespace POS_System.Data.Database
{
    public static class Seeder
    {
        public static void SeedAll(ModelBuilder modelBuilder)
        {
            SeedTax(modelBuilder);
            SeedProduct(modelBuilder);
            SeedProductOnTax(modelBuilder);
            SeedService(modelBuilder);
            SeedServiceOnTax(modelBuilder);
            SeedEmployees(modelBuilder);
            SeedCarts(modelBuilder);
            SeedCartItem(modelBuilder);
            SeedProductModification(modelBuilder);
        }

        public static void SeedTax(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tax>().HasData(
                new Tax { Id = 1, TaxId = 1, Name = "Tax1", Rate = 5, IsPercentage = true, Version = new DateTime(2024, 11, 1, 14, 0, 0), IsDeleted = true },
                new Tax { Id = 2, TaxId = 2, Name = "Tax2", Rate = 10, IsPercentage = true, Version = new DateTime(2024, 11, 1, 15, 0, 0), IsDeleted = false },
                new Tax { Id = 3, TaxId = 3, Name = "Tax3", Rate = 299, IsPercentage = false, Version = new DateTime(2024, 11, 1, 16, 0, 0), IsDeleted = true },
                new Tax { Id = 4, TaxId = 1, Name = "Tax1 v2", Rate = 199, IsPercentage = false, Version = new DateTime(2024, 11, 1, 20, 0, 0), IsDeleted = false }
            );
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

        public static void SeedProduct(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, ProductId = 1, Name = "Product1", Description = "P1 desc", Price = 1099, Stock = 10, ImageURL = "", Version = new DateTime(2024, 10, 1, 9, 0, 0), IsDeleted = true },
                new Product { Id = 2, ProductId = 2, Name = "Product2", Description = "P2 desc", Price = 199, Stock = 5, ImageURL = "", Version = new DateTime(2024, 10, 15, 15, 0, 0), IsDeleted = true },
                new Product { Id = 3, ProductId = 1, Name = "Product1 v2", Description = "P1 v2 desc", Price = 1099, Stock = 15, ImageURL = "", Version = new DateTime(2024, 10, 5, 16, 0, 0), IsDeleted = true },
                new Product { Id = 4, ProductId = 1, Name = "Product1 v3", Description = "P1 v2 desc", Price = 599, Stock = 7, ImageURL = "", Version = new DateTime(2024, 11, 1, 17, 0, 0), IsDeleted = false }
            );
        }

        public static void SeedProductModification(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductModification>().HasData(
                new ProductModification { Id = 1, ProductVersionId = 1, ProductModificationId = 1, Name = "Extra cheese", Description = "decs1", Price = 99, Version = new DateTime(2024, 10, 1, 9, 0, 0), IsDeleted = true }, 
                new ProductModification { Id = 2, ProductVersionId = 1, ProductModificationId = 1, Name = "Extra cheese v2", Description = "decs1", Price = 100, Version = new DateTime(2024, 11, 1, 9, 0, 0), IsDeleted = false },
                new ProductModification { Id = 3, ProductVersionId = 1, ProductModificationId = 2, Name = "No cheese", Description = "decs1", Price = 0, Version = new DateTime(2024, 11, 2, 9, 0, 0), IsDeleted = true }, 
                new ProductModification { Id = 4, ProductVersionId = 1, ProductModificationId = 2, Name = "No cheese v2", Description = "decs1", Price = 0, Version = new DateTime(2024, 11, 3, 9, 0, 0), IsDeleted = false },
                new ProductModification { Id = 5, ProductVersionId = 2, ProductModificationId = 3, Name = "Extra fork", Description = "decs1", Price = 50, Version = new DateTime(2024, 11, 4, 9, 0, 0), IsDeleted = false }
            );
        }

        public static void SeedService(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Service>().HasData(
                new Service { Id = 1, ServiceId = 1, Name = "Service1", Description = "S1 desc", Price = 2599, Duration = 45, ImageURL = "", Version = new DateTime(2024, 10, 16, 19, 0, 0), IsDeleted = false },
                new Service { Id = 2, ServiceId = 2, Name = "Service2", Description = "S2 desc", Price = 4599, Duration = 25, ImageURL = "", Version = new DateTime(2024, 10, 18, 12, 0, 0), IsDeleted = true },
                new Service { Id = 3, ServiceId = 3, Name = "Service3", Description = "S3 desc", Price = 1699, Duration = 10, ImageURL = "", Version = new DateTime(2024, 10, 19, 15, 0, 0), IsDeleted = true },
                new Service { Id = 4, ServiceId = 2, Name = "Service2 v2", Description = "S2 v2 desc", Price = 4099, Duration = 40, ImageURL = "", Version = new DateTime(2024, 11, 1, 15, 30, 0), IsDeleted = false }
            );
        }

        public static void SeedProductOnTax(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOnTax>().HasData(
                new ProductOnTax { ProductVersionId = 1, TaxVersionId = 1 },
                new ProductOnTax { ProductVersionId = 1, TaxVersionId = 4 },
                new ProductOnTax { ProductVersionId = 4, TaxVersionId = 4 },
                new ProductOnTax { ProductVersionId = 3, TaxVersionId = 3 }
            );
        }

        public static void SeedServiceOnTax(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceOnTax>().HasData(
                new ServiceOnTax { ServiceVersionId = 1, TaxVersionId = 4 },
                new ServiceOnTax { ServiceVersionId = 4, TaxVersionId = 1 },
                new ServiceOnTax { ServiceVersionId = 4, TaxVersionId = 4 },
                new ServiceOnTax { ServiceVersionId = 2, TaxVersionId = 3 }
            );
        }

        public static void SeedCartItem(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { Id = 1, CartId = 1, ProductVersionId = 1, ServiceVersionId = null, Quantity = 2, IsProduct = true },
                new CartItem { Id = 2, CartId = 1, ProductVersionId = null, ServiceVersionId = 1, Quantity = 1, IsProduct = false },
                new CartItem { Id = 3, CartId = 2, ProductVersionId = 2, ServiceVersionId = null, Quantity = 4, IsProduct = true },
                new CartItem { Id = 4, CartId = 2, ProductVersionId = 3, ServiceVersionId = null, Quantity = 10, IsProduct = true }
            );
        }

        public static void SeedCarts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().HasData(
                new Cart { Id = 1, EmployeeVersionId = 1, DateCreated = new DateTime(2024, 01, 01) },
                new Cart { Id = 2, EmployeeVersionId = 2, DateCreated = new DateTime(2024, 02, 01) },
                new Cart { Id = 3, EmployeeVersionId = 3, DateCreated = new DateTime(2024, 03, 01) },
                new Cart { Id = 4, EmployeeVersionId = 4, DateCreated = new DateTime(2024, 04, 01) }
            );
        }
    }
}
