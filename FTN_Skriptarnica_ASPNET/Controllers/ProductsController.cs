using FTN_Skriptarnica_ASPNET.Data;
using FTN_Skriptarnica_ASPNET.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FTN_Skriptarnica_ASPNET.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if(!ModelState.IsValid) return View(product);
            _contex.Products.Add(product);
            await _contex.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var p = await _contex.Products.FindAsync(id);
            return p == null ? NotFound() : View(p);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return BadRequest();

            if (!ModelState.IsValid) return View(product);

            _contex.Update(product);
            await _contex.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var p = await _contex.Products.FindAsync(id);
            return p == null ? NotFound() : View(p);
        }

        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var p = await _contex.Products.FindAsync(id);
            if (p != null)
            {
                _contex.Products.Remove(p);
                await _contex.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}