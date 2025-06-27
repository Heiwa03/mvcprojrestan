using BigHauling.Domain.Entities;
using BigHauling.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BigHauling.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IAdminAPI _adminAPI;

        public DashboardController()
        {
            var bl = new BusinessLogic.BusinessLogic();
            _adminAPI = bl.GetAdminAPI();
        }

        // GET: Admin/Dashboard
        public async Task<IActionResult> Index()
        {
            var trucks = await _adminAPI.GetAllTrucksAsync();
            return View(trucks);
        }

        // GET: Admin/Dashboard/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Dashboard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Truck truck)
        {
            if (ModelState.IsValid)
            {
                await _adminAPI.CreateTruckAsync(truck);
                return RedirectToAction(nameof(Index));
            }
            return View(truck);
        }

        // GET: Admin/Dashboard/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var truck = await _adminAPI.GetTruckByIdAsync(id.Value);
            if (truck == null) return NotFound();
            return View(truck);
        }

        // POST: Admin/Dashboard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Truck truck)
        {
            if (id != truck.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var success = await _adminAPI.UpdateTruckAsync(truck);
                if (!success) return NotFound();
                return RedirectToAction(nameof(Index));
            }
            return View(truck);
        }

        // GET: Admin/Dashboard/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var truck = await _adminAPI.GetTruckByIdAsync(id.Value);
            if (truck == null) return NotFound();
            return View(truck);
        }

        // POST: Admin/Dashboard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _adminAPI.DeleteTruckAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 