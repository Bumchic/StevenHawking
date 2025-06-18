using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SoftwareSellApp.Models;

namespace SoftwareSellApp.Controllers
{
    public class CartController : Controller
    {
        public readonly ILogger<CartController> logger;
        public CartController(ILogger<CartController> logger)
        {
            this.logger = logger;
        }
        [Authorize]
        public IActionResult CartView()
        {
            Cart cart;
            string? cartJson = HttpContext.Session.GetString("Cart");
            if(cartJson is null)
            {
                return View(new Cart());
            }
            cart = JsonConvert.DeserializeObject<Cart>(cartJson);
            return View(cart);
        }
    }
}
