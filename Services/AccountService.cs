using InventoryManagement.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Claims;
using System.Security.Cryptography;

namespace InventoryManagement.Services;

public class AccountService(IDataService dataService, IHttpContextAccessor httpContextAccessor) : IAccountService
{
    private const int SALT_SIZE = 16; // 128 bit
    private const int KEY_SIZE = 32; // 256 bit
    private const int ITERATIONS = 10000;

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
        User? user = users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

        if (user is not null && VerifyPassword(password, user.Password))
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
        user.Password = HashPassword(user.Password);
        dataService.AddUser(user);
        await LoginUserAsync(user.Email, user.Password);
    }

    public async Task LogoutUserAsync()
    {
        await httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
        byte[] hash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, ITERATIONS, KEY_SIZE);

        return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }

    private static bool VerifyPassword(string password, string hashedPassword)
    {
        string[] parts = hashedPassword.Split('.', 2);
        if (parts.Length != 2)
        {
            return false;
        }

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] hash = Convert.FromBase64String(parts[1]);

        byte[] hashToCompare = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, ITERATIONS, KEY_SIZE);

        return hashToCompare.SequenceEqual(hash);
    }
}
