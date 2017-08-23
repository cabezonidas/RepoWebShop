using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class PieDetailsListViewModel
    {
        public IEnumerable<PieDetailViewModel> PieDetails { get; set; }
        public string CurrentCategory { get; set; }
    }
}
