using FTN_Skriptarnica_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;

namespace FTN_Skriptarnica_ASPNET.Controllers
{
    public class ProductsController : Controller
    {
        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Osnove programiranja",
                    Category = "Knjige",
                    Price = 1200,
                    Description = "Udzbenik za ucenje osnova programiranja u C#",
                    StockQuantity = 15
                },
                new Product
                {
                    Id = 2,
                    Name = "Olovka",
                    Category = "Pribor",
                    Price = 30,
                    Description = "Olovka za pisanje, skolski pribor",
                    StockQuantity = 100
                },
                new Product
                {
                    Id = 3,
                    Name = "Majica",
                    Category = "Garderoba",
                    Price = 900,
                    Description = "Pamucna majica sa logom FTN-a",
                    StockQuantity = 50
                }
            };
        }

        public IActionResult Index()
        {
            return View(GetProducts());
        }

        public IActionResult Details(int id)
        {
            var product = GetProducts().FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}