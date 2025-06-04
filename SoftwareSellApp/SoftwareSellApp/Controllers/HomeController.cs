using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoftwareSellApp.Models;
using Microsoft.EntityFrameworkCore;

namespace SoftwareSellApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext db;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
    {
        _logger = logger;
        this.db = db;
    }

    [HttpGet]
    [HttpPost]
    public async Task<IActionResult> Index(string Name)
    {
        if (string.IsNullOrEmpty(Name))
        {
            return View(await db.products.ToListAsync());
        }

        return View(await db.products.Where(p => p.productName.Contains(Name)).ToListAsync());
    }

    public async Task<IActionResult> ProductView(string id)
    {
        Product product = await db.products.FirstOrDefaultAsync(p => p.productId == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    public IActionResult AddProduct()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(Product product)
    {
        if (ModelState.IsValid)
        {
            db.products.Add(product);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}