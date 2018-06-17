using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class LunchTicketViewModel : Lunch
    {
        public bool InStore { get; set; }
    }
}
