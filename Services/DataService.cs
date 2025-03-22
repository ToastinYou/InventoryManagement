using InventoryManagement.Data;
using InventoryManagement.Models;

namespace InventoryManagement.Services;

public class DataService(AppDbContext context) : IDataService
{
    public List<User> GetUsers()
    {
        return [.. context.Users];
    }

    public void AddUser(User user)
    {
        context.Add(user);
        context.SaveChanges();
    }

    public List<Product> GetProducts()
    {
        return [.. context.Products];
    }
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
    }

    public void UpdateQuantity(string sku, int quantity)
    {
        Product product = context.Products.Single(p => p.Sku == sku);
        product.Quantity = quantity;
        context.SaveChanges();
    }

    public void DeleteProduct(string sku)
    {
        context.Products.Remove(context.Products.Single(p => p.Sku == sku));
    }
}
