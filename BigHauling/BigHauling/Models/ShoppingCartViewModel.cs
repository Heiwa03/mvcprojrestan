using System.Collections.Generic;
using System.Linq;

namespace BigHauling.Models
{
    public class ShoppingCartViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();

        public decimal Total => Items.Sum(item => item.Truck.Price * item.Quantity);
    }
} 