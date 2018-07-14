using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepoWebShop.Models;

namespace RepoWebShop.ViewModels
{
    public class VisitsViewModel
    {
        public IEnumerable<PageVisit> Visits { get; internal set; }
        public IEnumerable<KeyValuePair<string, int>> ByPath { get; internal set; }
        public IEnumerable<KeyValuePair<string, int>> ByIp { get; internal set; }
    }
}
