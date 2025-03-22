using InventoryManagement.Models;

namespace InventoryManagement.Services;

public interface IInventoryService
{
    List<Product> GetProducts();
    void AddProduct(Product product);
    void UpdateQuantity(string sku, int quantity);
    void DeleteProduct(string sku);
}
