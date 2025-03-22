using InventoryManagement.Models;

namespace InventoryManagement.Data;

public static class DbInitializer
{
    public static void Initialize(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
        {
            // DB has been seeded.
            return;
        }

        context.Users.AddRange(
        [
            new User("admin@im.com", "admin")
        ]);
        context.SaveChanges();

        context.Products.AddRange(
        [
            new Product { Sku = "SKU001", Name = "Product 1", Quantity = 100 },
            new Product { Sku = "SKU002", Name = "Product 2", Quantity = 200 },
            new Product { Sku = "SKU003", Name = "Product 3", Quantity = 150 },
            new Product { Sku = "SKU004", Name = "Product 4", Quantity = 50 },
            new Product { Sku = "SKU005", Name = "Product 5", Quantity = 75 },
            new Product { Sku = "SKU006", Name = "Product 6", Quantity = 300 },
            new Product { Sku = "SKU007", Name = "Product 7", Quantity = 120 },
            new Product { Sku = "SKU008", Name = "Product 8", Quantity = 85 },
            new Product { Sku = "SKU009", Name = "Product 9", Quantity = 210 },
            new Product { Sku = "SKU010", Name = "Product 10", Quantity = 60 }
        ]);
        context.SaveChanges();
    }
}
