using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElectroniX.Data;
using ElectroniX.Models;
using ElectroniX.Models.Enums;

namespace ElectroniX.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Products
        public async Task<IActionResult> Index(UsageType? usage = null, StudyCourse? course = null)
        {
            var products = _context.Products.AsQueryable();

            // Filter by UsageType
            if (usage.HasValue)
                products = products.Where(p => p.RecommendedUsage == usage);

            // If study, filter by course
            if (course.HasValue)
                products = products.Where(p => p.RecommendedCourse == course);

            var model = await products.ToListAsync();
            return View(model);
        }

        // GET: /Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            return View(product);
        }
    }
}
