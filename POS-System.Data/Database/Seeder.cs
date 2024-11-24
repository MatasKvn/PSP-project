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

        public static void SeedProduct(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, ProductId = 1, Name = "Product1", Description = "P1 desc", Price = 1099, Stock = 10, ImageURL = "", Version = new DateTime(2024, 10, 1, 9, 0, 0), IsDeleted = true },
                new Product { Id = 2, ProductId = 2, Name = "Product2", Description = "P2 desc", Price = 199, Stock = 5, ImageURL = "", Version = new DateTime(2024, 10, 15, 15, 0, 0), IsDeleted = true },
                new Product { Id = 3, ProductId = 1, Name = "Product1 v2", Description = "P1 v2 desc", Price = 1099, Stock = 15, ImageURL = "", Version = new DateTime(2024, 10, 5, 16, 0, 0), IsDeleted = true },
                new Product { Id = 4, ProductId = 1, Name = "Product1 v3", Description = "P1 v2 desc", Price = 599, Stock = 7, ImageURL = "", Version = new DateTime(2024, 11, 1, 17, 0, 0), IsDeleted = false }
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
    }
}
