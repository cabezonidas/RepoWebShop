using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<PieDetailViewModel> PiesOfTheWeek { get; set; }
        public string HostUrl { get; internal set; }
    }
}
