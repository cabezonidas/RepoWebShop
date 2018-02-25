using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class UserOrdersViewModel
    {
        public ApplicationUser User { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
