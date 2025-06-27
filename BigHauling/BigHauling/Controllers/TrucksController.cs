using Microsoft.AspNetCore.Mvc;
using BigHauling.Models;
using BigHauling.Domain.Entities;
using BigHauling.BusinessLogic.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BigHauling.Controllers
{
    public class TrucksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrucksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trucks
        public async Task<IActionResult> Index(string searchTerm, string brand, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchTerm;
            ViewData["CurrentBrand"] = brand;
            ViewData["CurrentSort"] = sortOrder;

            var trucksQuery = _context.Trucks.Where(t => t.IsAvailable);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                trucksQuery = trucksQuery.Where(t => t.Name.Contains(searchTerm) || t.Description.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(brand))
            {
                trucksQuery = trucksQuery.Where(t => t.Brand == brand);
            }

            switch (sortOrder)
            {
                case "price_desc":
                    trucksQuery = trucksQuery.OrderByDescending(t => t.Price);
                    break;
                case "price_asc":
                    trucksQuery = trucksQuery.OrderBy(t => t.Price);
                    break;
                case "year_desc":
                    trucksQuery = trucksQuery.OrderByDescending(t => t.Year);
                    break;
                default:
                    trucksQuery = trucksQuery.OrderByDescending(t => t.CreatedAt);
                    break;
            }
            
            var trucks = await trucksQuery.ToListAsync();
            return View(trucks);
        }

        // GET: Trucks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Trucks.FirstOrDefaultAsync(m => m.Id == id);
            
            if (truck == null)
            {
                return NotFound();
            }

            ViewBag.SimilarTrucks = await _context.Trucks
                .Where(t => t.Brand == truck.Brand && t.Id != truck.Id && t.IsAvailable)
                .Take(3)
                .ToListAsync();

            return View(truck);
        }

        public IActionResult Search(string q)
        {
            return RedirectToAction(nameof(Index), new { searchTerm = q });
        }
    }
} 