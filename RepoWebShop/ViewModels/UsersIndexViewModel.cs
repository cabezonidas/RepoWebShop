using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class UsersIndexViewModel
    {
        public IEnumerable<Order> OrdersWithoutUsers { get; set; }
        public IEnumerable<Order> OrdersWithUsers { get; set;}
        public IEnumerable<ApplicationUser> UsersThatOrdered { get; set; }
        public IEnumerable<ApplicationUser> UsersThatDidntOrder { get; set; }
    }
}
