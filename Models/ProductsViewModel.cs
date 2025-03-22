namespace InventoryManagement.Models;

public class ProductsViewModel
{
    public List<Product> Products { get; set; } = [];
    public Product Product { get; set; } = new();
}
