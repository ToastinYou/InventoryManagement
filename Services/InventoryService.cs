using InventoryManagement.Models;

namespace InventoryManagement.Services;

public class InventoryService(IDataService dataService) : IInventoryService
{
    public List<Product> GetProducts()
    {
        return dataService.GetProducts();
    }

    public void AddProduct(Product product)
    {
        dataService.AddProduct(product);
    }

    public void UpdateQuantity(string sku, int quantity)
    {
        dataService.UpdateQuantity(sku, quantity);
    }

    public void DeleteProduct(string sku)
    {
        dataService.DeleteProduct(sku);
    }
}
