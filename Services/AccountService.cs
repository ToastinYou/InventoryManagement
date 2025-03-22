using InventoryManagement.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace InventoryManagement.Services;

public class AccountService(IDataService dataService, IHttpContextAccessor httpContextAccessor) : IAccountService
{
    public User? ActiveUser
    {
        get
        {
            string? email = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            if (email is not null)
            {
                return dataService.GetUsers().FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            }

            return null;
        }
    }

    public async Task<bool> LoginUserAsync(string email, string password)
    {
        List<User> users = dataService.GetUsers();
        User? user = users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Password == password);

        if (user is not null)
        {
            List<Claim> claims =
            [
                new(ClaimTypes.Name, user.Email)
            ];

            ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties authProperties = new()
            {
                IsPersistent = true
            };

            await httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), authProperties);

            return true;
        }

        return false;
    }

    public async Task RegisterUserAsync(User user)
    {
        dataService.AddUser(user);
        await LoginUserAsync(user.Email, user.Password);
    }

    public async Task LogoutUserAsync()
    {
        await httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
