using System.Diagnostics;
using BigHauling.Models;
using Microsoft.AspNetCore.Mvc;
using BigHauling.Domain.Entities;
using BigHauling.BusinessLogic.Data;
using Microsoft.EntityFrameworkCore;

namespace BigHauling.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.FeaturedTrucks = await _context.Trucks
                .Where(t => t.IsAvailable)
                .OrderByDescending(t => t.CreatedAt)
                .Take(3)
                .ToListAsync();
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
