using FTN_Skriptarnica_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.Json;

namespace FTN_Skriptarnica_ASPNET.Controllers
{
    public class CartController : Controller
    {
        private const string CartKey = "Cart";

        //Citanje korpe
        private List<CartItem> GetCart()
        {
            var cartJson = HttpContext.Session.GetString(CartKey);
            if (string.IsNullOrEmpty(cartJson))
            {
                return new List<CartItem>();
            }

            return JsonSerializer.Deserialize<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        //Cuvanje korpe
        private void SaveCart(List<CartItem> cart)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CartKey, cartJson);
        }

        //Prikaz korpe
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        //Dodavanje u korpu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int productId)
        {
            var product = ProductsController.GetProducts().FirstOrDefault(p => p.Id == productId);

            if (product == null)
            {
                return NotFound();
            }

            var cart = GetCart();

            var item = cart.FirstOrDefault(x => x.ProductId == productId);

            int currentQuantity = item?.Quantity ?? 0;

            if(currentQuantity >= product.StockQuantity)
            {
                TempData["Error"] = "Nema dovoljno proizvoda na stanju.";
                return RedirectToAction("Index", "Cart");
            }

            if(item == null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }
            else
            {
                item.Quantity++;
            }
            SaveCart(cart);
            TempData["Success"] = "Proizvod je dodat u korpu.";
            return RedirectToAction("Index");
        }
    }
}
