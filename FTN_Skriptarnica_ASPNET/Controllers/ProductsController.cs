using FTN_Skriptarnica_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace FTN_Skriptarnica_ASPNET.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = new List<Product>
            { 
                new Product { Id = 1, Name = "Osnove programiranja", Category = "Knjige", Price = 1200 },
                new Product { Id = 2, Name = "Olovka", Category = "Pribor", Price = 30 },
                new Product { Id = 3, Name = "Majica", Category = "Garderoba", Price= 800 }
            };
            return View(products);
        }
    }
}
