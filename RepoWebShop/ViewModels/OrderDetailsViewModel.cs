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
        private UserManager<ApplicationUser> _userManager;

        public OrderDetailsViewModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public Order Order { get; set; }
        public IEnumerable<OrderDetail> Items { get; set; }
    }
}
