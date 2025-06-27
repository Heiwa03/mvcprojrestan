using System.Diagnostics;
using BigHauling.Models;
using Microsoft.AspNetCore.Mvc;
using BigHauling.BusinessLogic.Interfaces;
using System.Threading.Tasks;

namespace BigHauling.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITruckService _truckService;

        public HomeController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _truckService = bl.GetTruckService();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.FeaturedTrucks = await _truckService.GetFeaturedTrucksAsync();
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
