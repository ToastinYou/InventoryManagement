using InventoryManagement.Models;

namespace InventoryManagement.Services;

public interface IAccountService
{
    User? ActiveUser { get; }
    Task<bool> LoginUserAsync(string email, string password);
    Task RegisterUserAsync(User user);
    Task LogoutUserAsync();
}
