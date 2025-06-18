using Microsoft.AspNetCore.Mvc;
using SoftwareSellApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SoftwareSellApp.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public const string CARTKEY = "cart";

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CARTKEY) ?? new List<CartItem>();
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _context.products.FirstOrDefault(p => p.productId == id);
            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại");
            }

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CARTKEY) ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(item => item.productId == id);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
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

        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CARTKEY) ?? new List<CartItem>();
            var cartItem = cart.FirstOrDefault(item => item.productId == id);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
            }

            HttpContext.Session.SetObjectAsJson(CARTKEY, cart);

            return RedirectToAction("Index");
        }
    }
}