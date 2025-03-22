using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers;
public class AccountController(IAccountService accountService) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        if (accountService.ActiveUser is not null)
        {
            return RedirectToAction("Index", "Products");
        }

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(AccountViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (await accountService.LoginUserAsync(model.Email, model.Password))
        {
            return RedirectToAction("Index", "Products");
        }

        ModelState.AddModelError("Password", "Invalid email or password.");
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterAsync(AccountViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        await accountService.RegisterUserAsync(new User(model.Email, model.Password));
        return RedirectToAction("Index", "Products");
    }

    [HttpGet]
    public async Task<IActionResult> LogoutAsync()
    {
        await accountService.LogoutUserAsync();
        return RedirectToAction("Login");
    }
}
