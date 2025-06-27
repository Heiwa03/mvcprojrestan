using Microsoft.AspNetCore.Mvc;
using BigHauling.BusinessLogic.Interfaces;
using System.Threading.Tasks;

namespace BigHauling.Controllers
{
    public class TrucksController : Controller
    {
        private readonly ITruckService _truckService;

        public TrucksController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _truckService = bl.GetTruckService();
        }

        // GET: Trucks
        public async Task<IActionResult> Index(string searchTerm, string brand, string sortOrder)
        {
            ViewData["CurrentFilter"] = searchTerm;
            ViewData["CurrentBrand"] = brand;
            ViewData["CurrentSort"] = sortOrder;
            
            var trucks = await _truckService.GetAllAvailableTrucksAsync(searchTerm, brand, sortOrder);
            return View(trucks);
        }

        // GET: Trucks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _truckService.GetTruckDetailsAsync(id.Value);
            
            if (truck == null)
            {
                return NotFound();
            }

            ViewBag.SimilarTrucks = await _truckService.GetSimilarTrucksAsync(truck);

            return View(truck);
        }

        public IActionResult Search(string q)
        {
            return RedirectToAction(nameof(Index), new { searchTerm = q });
        }
    }
} 