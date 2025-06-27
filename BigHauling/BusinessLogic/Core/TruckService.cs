using BigHauling.BusinessLogic.Data;
using BigHauling.BusinessLogic.Interfaces;
using BigHauling.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigHauling.BusinessLogic.Core
{
    public class TruckService : ITruckService
    {
        private readonly ApplicationDbContext _context;

        public TruckService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Truck>> GetFeaturedTrucksAsync()
        {
            return await _context.Trucks
                .Where(t => t.IsAvailable)
                .OrderByDescending(t => t.CreatedAt)
                .Take(3)
                .ToListAsync();
        }

        public async Task<List<Truck>> GetAllAvailableTrucksAsync(string searchTerm, string brand, string sortOrder)
        {
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

            return await trucksQuery.ToListAsync();
        }

        public async Task<Truck?> GetTruckDetailsAsync(int id)
        {
            return await _context.Trucks.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Truck>> GetSimilarTrucksAsync(Truck truck)
        {
            return await _context.Trucks
                .Where(t => t.Brand == truck.Brand && t.Id != truck.Id && t.IsAvailable)
                .Take(3)
                .ToListAsync();
        }
    }
} 