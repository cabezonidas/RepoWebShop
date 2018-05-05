using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RepoWebShop.Models;

namespace RepoWebShop.ViewModels
{
    public class VisitsViewModel
    {
        public List<PageVisit> Visits { get; internal set; }
        public List<KeyValuePair<string, int>> ByPath { get; internal set; }
        public List<KeyValuePair<string, int>> ByIp { get; internal set; }
    }
}
