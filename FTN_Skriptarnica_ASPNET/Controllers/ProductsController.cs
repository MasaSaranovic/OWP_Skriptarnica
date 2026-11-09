using FTN_Skriptarnica_ASPNET.Data;
using FTN_Skriptarnica_ASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FTN_Skriptarnica_ASPNET.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _contex;

        public ProductsController(ApplicationDbContext contex)
        {
            _contex = contex;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _contex.Products
                .AsNoTracking()
                .ToListAsync();

            return View(products);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _contex.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}