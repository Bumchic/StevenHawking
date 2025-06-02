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
    public IActionResult Index()
    {
        return View(db.products);
    }

    public async Task<IActionResult> Index(string Name)
    {
        return View(await db.products.Where(p => p.productName.Contains(Name)).ToListAsync());
    }

    public async Task<IActionResult> ProductView(int id)
    {
        Product product = await db.products.FindAsync(id);
        return View(product);
    }
    public async Task<IActionResult> AddProduct()
    {
        return null;
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
