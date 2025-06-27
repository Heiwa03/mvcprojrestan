using BigHauling.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigHauling.BusinessLogic.Interfaces
{
    public interface ITruckService
    {
        Task<List<Truck>> GetFeaturedTrucksAsync();
        Task<List<Truck>> GetAllAvailableTrucksAsync(string searchTerm, string brand, string sortOrder);
        Task<Truck?> GetTruckDetailsAsync(int id);
        Task<List<Truck>> GetSimilarTrucksAsync(Truck truck);
    }
} 