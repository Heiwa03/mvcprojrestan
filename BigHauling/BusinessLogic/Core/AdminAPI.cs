using BigHauling.BusinessLogic.Data;
using BigHauling.BusinessLogic.Interfaces;
using BigHauling.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigHauling.BusinessLogic.Core
{
    public class AdminAPI : IAdminAPI
    {
        private readonly ApplicationDbContext _context;

        public AdminAPI(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Truck>> GetAllTrucksAsync()
        {
            return await _context.Trucks.ToListAsync();
        }

        public async Task<Truck?> GetTruckByIdAsync(int id)
        {
            return await _context.Trucks.FindAsync(id);
        }

        public async Task CreateTruckAsync(Truck truck)
        {
            _context.Add(truck);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateTruckAsync(Truck truck)
        {
            _context.Update(truck);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TruckExists(truck.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteTruckAsync(int id)
        {
            var truck = await _context.Trucks.FindAsync(id);
            if (truck == null)
            {
                return false;
            }

            _context.Trucks.Remove(truck);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> TruckExists(int id)
        {
            return await _context.Trucks.AnyAsync(e => e.Id == id);
        }
    }
}
