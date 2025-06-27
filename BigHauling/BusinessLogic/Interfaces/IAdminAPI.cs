using BigHauling.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigHauling.BusinessLogic.Interfaces
{
    public interface IAdminAPI
    {
        Task<List<Truck>> GetAllTrucksAsync();
        Task<Truck?> GetTruckByIdAsync(int id);
        Task CreateTruckAsync(Truck truck);
        Task<bool> UpdateTruckAsync(Truck truck);
        Task<bool> DeleteTruckAsync(int id);
    }
} 