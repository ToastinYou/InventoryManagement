using InventoryManagement.Models;

namespace InventoryManagement.Services;

public interface IDataService
{
    List<User> GetUsers();
    void AddUser(User user);
    List<Product> GetProducts();
    void AddProduct(Product product);
    void UpdateQuantity(string sku, int quantity);
    void DeleteProduct(string sku);
}
