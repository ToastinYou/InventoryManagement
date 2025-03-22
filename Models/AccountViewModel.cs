using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models;

public class AccountViewModel
{
    [Required]
    public string Email { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}
