using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace RepoWebShop.ViewModels
{
    public class OrderDetailsViewModel
    {
        private Order _order;
        private IEnumerable<OrderDetail> _items;

        public OrderDetailsViewModel(Order order, IEnumerable<OrderDetail> items)
        {
            _order = order;
            _items = items;
        }

        public Order Order
        {
            get
            {
                return _order;
            }
        }
        public IEnumerable<OrderDetail> Items
        {
            get
            {
                return _items;
            }
        }
    }
}
