using System.Diagnostics;
using BigHauling.Models;
using Microsoft.AspNetCore.Mvc;

namespace BigHauling.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // Sample featured trucks for the homepage
            var featuredTrucks = new List<Truck>
            {
                new Truck
                {
                    Id = 1,
                    Name = "2022 Freightliner Cascadia",
                    Description = "Excellent condition, low mileage, perfect for long-haul operations.",
                    Price = 85000.00m,
                    Brand = "Freightliner",
                    Model = "Cascadia",
                    Year = 2022,
                    EngineType = "Detroit DD15",
                    Transmission = "Automatic",
                    Mileage = 125000,
                    FuelType = "Diesel",
                    Condition = "Excellent",
                    ImageUrl = "/images/truck1.jpg",
                    IsAvailable = true
                },
                new Truck
                {
                    Id = 2,
                    Name = "2021 Peterbilt 579",
                    Description = "Well-maintained, fuel-efficient, ready for immediate delivery.",
                    Price = 92000.00m,
                    Brand = "Peterbilt",
                    Model = "579",
                    Year = 2021,
                    EngineType = "PACCAR MX-13",
                    Transmission = "Manual",
                    Mileage = 98000,
                    FuelType = "Diesel",
                    Condition = "Very Good",
                    ImageUrl = "/images/truck2.jpg",
                    IsAvailable = true
                },
                new Truck
                {
                    Id = 3,
                    Name = "2023 Kenworth T680",
                    Description = "Brand new, latest technology, premium comfort features.",
                    Price = 125000.00m,
                    Brand = "Kenworth",
                    Model = "T680",
                    Year = 2023,
                    EngineType = "PACCAR MX-13",
                    Transmission = "Automatic",
                    Mileage = 15000,
                    FuelType = "Diesel",
                    Condition = "New",
                    ImageUrl = "/images/truck3.jpg",
                    IsAvailable = true
                }
            };

            ViewBag.FeaturedTrucks = featuredTrucks;
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
