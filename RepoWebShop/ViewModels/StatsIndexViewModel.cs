using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class StatsIndexViewModel
    {
        public decimal TotalReservations { get; internal set; }
        public decimal TotalMercadoPago { get; internal set; }
        public decimal TotalIncome { get; internal set; }
        public IEnumerable<KeyValuePair<PieDetail, int>> ProductsMostAddedToTrolley { get; set; }
        public IEnumerable<KeyValuePair<Pie, int>> ItemsMostAddedToTrolley { get; set; }
        public IEnumerable<KeyValuePair<PieDetail, int>> ProductsMostPurchased { get; set; }
        public IEnumerable<KeyValuePair<Pie, int>> ItemsMostPurchased { get; set; }
    }
}
