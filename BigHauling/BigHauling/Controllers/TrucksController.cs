using Microsoft.AspNetCore.Mvc;
using BigHauling.Models;

namespace BigHauling.Controllers
{
    public class TrucksController : Controller
    {
        private readonly ILogger<TrucksController> _logger;

        public TrucksController(ILogger<TrucksController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string? searchTerm, string? brand, string? condition, decimal? minPrice, decimal? maxPrice)
        {
            // Sample truck inventory data
            var trucks = new List<Truck>
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
                },
                new Truck
                {
                    Id = 4,
                    Name = "2020 Volvo VNL 860",
                    Description = "Reliable workhorse, great fuel economy, perfect for regional hauls.",
                    Price = 75000.00m,
                    Brand = "Volvo",
                    Model = "VNL 860",
                    Year = 2020,
                    EngineType = "Volvo D13",
                    Transmission = "Automatic",
                    Mileage = 180000,
                    FuelType = "Diesel",
                    Condition = "Good",
                    ImageUrl = "/images/truck4.jpg",
                    IsAvailable = true
                },
                new Truck
                {
                    Id = 5,
                    Name = "2021 International LT",
                    Description = "Comfortable cab, excellent visibility, ideal for city and highway driving.",
                    Price = 68000.00m,
                    Brand = "International",
                    Model = "LT",
                    Year = 2021,
                    EngineType = "Cummins ISX15",
                    Transmission = "Manual",
                    Mileage = 110000,
                    FuelType = "Diesel",
                    Condition = "Very Good",
                    ImageUrl = "/images/truck5.jpg",
                    IsAvailable = true
                },
                new Truck
                {
                    Id = 6,
                    Name = "2022 Mack Anthem",
                    Description = "Built tough, reliable performance, perfect for heavy-duty applications.",
                    Price = 95000.00m,
                    Brand = "Mack",
                    Model = "Anthem",
                    Year = 2022,
                    EngineType = "Mack MP8",
                    Transmission = "Automatic",
                    Mileage = 85000,
                    FuelType = "Diesel",
                    Condition = "Excellent",
                    ImageUrl = "/images/truck6.jpg",
                    IsAvailable = false
                }
            };

            // Apply filters
            var filteredTrucks = trucks.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                filteredTrucks = filteredTrucks.Where(t => 
                    t.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    t.Brand.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    t.Model.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    t.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(brand))
            {
                filteredTrucks = filteredTrucks.Where(t => t.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(condition))
            {
                filteredTrucks = filteredTrucks.Where(t => t.Condition.Equals(condition, StringComparison.OrdinalIgnoreCase));
            }

            if (minPrice.HasValue)
            {
                filteredTrucks = filteredTrucks.Where(t => t.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filteredTrucks = filteredTrucks.Where(t => t.Price <= maxPrice.Value);
            }

            // Get unique brands and conditions for filter dropdowns
            ViewBag.Brands = trucks.Select(t => t.Brand).Distinct().OrderBy(b => b).ToList();
            ViewBag.Conditions = trucks.Select(t => t.Condition).Distinct().OrderBy(c => c).ToList();
            ViewBag.SearchTerm = searchTerm;
            ViewBag.SelectedBrand = brand;
            ViewBag.SelectedCondition = condition;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;

            return View(filteredTrucks.ToList());
        }

        public IActionResult Details(int id)
        {
            // Sample truck data - in real app, this would come from database
            var trucks = new List<Truck>
            {
                new Truck
                {
                    Id = 1,
                    Name = "2022 Freightliner Cascadia",
                    Description = "This 2022 Freightliner Cascadia is in excellent condition with low mileage, making it perfect for long-haul operations. The truck features a powerful Detroit DD15 engine with automatic transmission, providing smooth and efficient performance. The cab is spacious and comfortable, equipped with modern amenities for driver comfort during long journeys. The truck has been well-maintained and comes with complete service records.",
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
                }
            };

            var truck = trucks.FirstOrDefault(t => t.Id == id);
            
            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        public IActionResult Search(string q)
        {
            return RedirectToAction(nameof(Index), new { searchTerm = q });
        }
    }
} 