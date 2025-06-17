using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoftwareSellApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Identity;

namespace SoftwareSellApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext db;
    private readonly UserManager<User> userManager;
    private readonly RoleManager<IdentityRole> roleManager;


    public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        this.db = db;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    //public async Task<IActionResult> Index(string search = "")
    //{
    //    search = search.ToLower();
    //    List<Product> products = await db.products.ToListAsync();
    //    List<Product> filter = products.Where(p => p.productName.ToLower().Contains(search)).ToList();
    //    return View(filter);
    //}

    public async Task<IActionResult> Index(string search = "")
    {
<<<<<<< HEAD
        ViewBag.SearchTerm = search;
        IQueryable<Product> productsQuery = db.products.Include(p => p.Category);

        if (!string.IsNullOrEmpty(search))
        {
            string lowerSearch = search.ToLower();

            productsQuery = productsQuery.Where(p => p.productName != null && p.productName.ToLower().Contains(lowerSearch));
        }

        var filteredProducts = await productsQuery.ToListAsync();

        return View(filteredProducts);
=======
        Debug.WriteLine(User.IsInRole("Admin"));
        search = search.ToLower();
        List<Product> products = await db.products.ToListAsync();
        List<Product> filter = products.Where(p => p.productName.ToLower().Contains(search)).ToList();
        return View(filter);
>>>>>>> main
    }

    public async Task<IActionResult> ProductView(int id)
    {
        Product product = await db.products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddProductAsync()
    {
        List<Category> categories = await db.categories.ToListAsync();
        ViewBag.Categories = new SelectList(categories);
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

    //[HttpGet]
    //public IActionResult AddProduct()
    //{
    //    ViewBag.Categories = new SelectList(db.categories, "categoryId", "categoryName");
    //    return View();
    //}

    //[HttpPost]
    //public async Task<IActionResult> AddProduct(Product product)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        db.products.Add(product);
    //        await db.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }

    //    ViewBag.Categories = new SelectList(db.categories, "categoryId", "categoryName");
    //    return View(product);
    //}


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public ActionResult Categories()
    {
        var categories = db.categories.ToList();
        return View(categories);
    }

    public IActionResult Category(int id, string categoryName)
    {
        var products = db.products
                               .Include(p => p.Category)
                               .Where(p => p.Category.categoryId == id)
                               .ToList();
        ViewBag.CategoryName = categoryName;
        return View(products);
    }
    public async Task<IActionResult> ClaimAdmin()
    {
        User? user = await userManager.GetUserAsync(HttpContext.User);
        if(user is null)
        {
            return RedirectToAction("Index");
        }
        string? email = user.Email;
        if (!email.Equals("Administrator@Example.com"))
        {
            return RedirectToAction("Index");
        }
        bool result = await roleManager.RoleExistsAsync("Admin");
        if (!result)
        {
            IdentityResult roleResult = await roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Admin"
            });
            if (!roleResult.Succeeded)
            {
                RedirectToAction("Index");
            }
        }
        IList<User> adminUser = await userManager.GetUsersInRoleAsync("Admin");
        if (adminUser.Count == 0)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
        return RedirectToAction("Index");
    }
}