using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SoftwareSellApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareSellApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext db;

        public CartController(ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<User> userManager, ApplicationDbContext db)
        {
            _context = context;
            this.userManager = userManager;
            this.db = db;
        }

        public const string CARTKEY = "cart";
        [Authorize]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CARTKEY) ?? new Cart();
            return View(cart);
        }
        [Authorize]
        public IActionResult AddToCart(int id)
        {
            var product = _context.products.FirstOrDefault(p => p.productId == id);
            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại");
            }

            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CARTKEY) ?? new Cart();
            var cartItem = cart.listOfCartItem.FirstOrDefault(item => item.productId == id);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.listOfCartItem.Add(new CartItem
                {
                    productId = product.productId,
                    productName = product.productName,
                    price = product.price,
                    Quantity = 1,
                    images = product.images
                });
            }

            HttpContext.Session.SetObjectAsJson(CARTKEY, cart);

            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<Cart>(CARTKEY) ?? new Cart();
            var cartItem = cart.listOfCartItem.FirstOrDefault(item => item.productId == id);

            if (cartItem != null)
            {
                cart.listOfCartItem.Remove(cartItem);
            }

            HttpContext.Session.SetObjectAsJson(CARTKEY, cart);

            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> ProcessPayment()
        {
            Cart cart = HttpContext.Session.GetObjectFromJson<Cart>(CARTKEY);
            User user = await userManager.GetUserAsync(HttpContext.User);
            foreach (CartItem item in cart.listOfCartItem)
            {
                Order order = new Order()
                {
                    DayBought = DateTime.Now,
                    User = user,
                    TotalAmount = item.Total
                };
                await db.Orders.AddAsync(order);
                db.SaveChanges();
            }
            return RedirectToAction("paymentSuccess");
        }
        public async Task<IActionResult> paymentSuccess()
        {
            return View();
        }
    }

}