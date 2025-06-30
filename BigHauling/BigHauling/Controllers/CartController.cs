using BigHauling.Helpers;
using BigHauling.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace BigHauling.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "ShoppingCart";

        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<ShoppingCartViewModel>(CartSessionKey) ?? new ShoppingCartViewModel();
            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int truckId)
        {
            var bl = new BusinessLogic.BusinessLogic();
            var truckService = bl.GetTruckService();
            var truck = truckService.GetTruckDetailsAsync(truckId).Result; 

            if (truck == null)
            {
                return NotFound();
            }

            var cart = HttpContext.Session.Get<ShoppingCartViewModel>(CartSessionKey) ?? new ShoppingCartViewModel();

            var cartItem = cart.Items.FirstOrDefault(i => i.Truck.Id == truckId);
            if (cartItem == null)
            {
                cart.Items.Add(new CartItemViewModel { Truck = truck, Quantity = 1 });
            }
            else
            {
                // For now, we only allow one of each truck. In the future, you could increment quantity.
            }

            HttpContext.Session.Set(CartSessionKey, cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int truckId)
        {
            var cart = HttpContext.Session.Get<ShoppingCartViewModel>(CartSessionKey) ?? new ShoppingCartViewModel();

            var itemToRemove = cart.Items.FirstOrDefault(i => i.Truck.Id == truckId);
            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
            }

            HttpContext.Session.Set(CartSessionKey, cart);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.Get<ShoppingCartViewModel>(CartSessionKey) ?? new ShoppingCartViewModel();
            if (!cart.Items.Any())
            {
                // Optionally, redirect to the cart page with a message that the cart is empty
                return RedirectToAction("Index");
            }
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitOrder()
        {
            var cart = HttpContext.Session.Get<ShoppingCartViewModel>(CartSessionKey);
            if (cart == null || !cart.Items.Any())
            {
                return RedirectToAction("Index");
            }

            // Get the current user
            var bl = new BusinessLogic.BusinessLogic();
            var userManager = bl.GetUserManager();
            var user = await userManager.GetUserAsync(User);

            if (user == null)
            {
                return Challenge(); // Not logged in, redirect to login
            }
            
            // Create the order
            var order = new Domain.Entities.Order
            {
                ApplicationUserId = user.Id,
                OrderDate = DateTime.Now,
                OrderTotal = cart.Total,
                OrderStatus = "Pending",
                OrderDetails = new List<Domain.Entities.OrderDetail>()
            };

            // Create the order details and add them to the order
            foreach (var item in cart.Items)
            {
                order.OrderDetails.Add(new Domain.Entities.OrderDetail
                {
                    TruckId = item.Truck.Id,
                    Price = item.Truck.Price
                });
            }

            // Save the order using the BusinessLogic facade
            await bl.SaveOrderAsync(order);

            // Clear the cart
            HttpContext.Session.Remove(CartSessionKey);

            return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
        }

        [HttpGet]
        public IActionResult OrderConfirmation(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
    }
} 