using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Lunch
    {
        public int LunchId { get; set; }

        public IEnumerable<LunchItem> Items { get; set; }

        public IEnumerable<LunchMiscellaneous> Miscellanea { get; set; }

        public string Comments { get; set; }
    }
}
