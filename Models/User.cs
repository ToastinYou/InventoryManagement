using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models;

public class User(string email, string password)
{
    [Key]
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}
