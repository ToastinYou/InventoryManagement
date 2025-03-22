using System.Diagnostics;
using InventoryManagement.Models;
using InventoryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Controllers;

[Authorize]
public class ProductsController(IDataService dataService) : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        ProductsViewModel model = new()
        {
            Products = dataService.GetProducts()
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(ProductsViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View("Create", model);
        }

        dataService.AddProduct(model.Product);
        return RedirectToAction("Index");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult UpdateQuantities(ProductsViewModel model)
    {
        foreach (Product product in model.Products)
        {
            dataService.UpdateQuantity(product.Sku, product.Quantity);
        }

        return RedirectToAction("Index");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
