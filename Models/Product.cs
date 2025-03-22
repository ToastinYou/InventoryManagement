using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models;

public class Product
{
    [Key]
    [Required]
    [Display(Name = "SKU")]
    public string Sku { get; set; } = string.Empty;
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public int Quantity { get; set; }
}
