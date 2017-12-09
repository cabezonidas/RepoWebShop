using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Album
    {
        public long SetId { get; set; }
        public string Title { get; set; }
        public DateTime LastRefresh { get; set; }
        public IEnumerable<string> Urls { get; set; }
        public Album(long setId, string title)
        {
            SetId = setId;
            Title = title;
            LastRefresh = DateTime.Now;
            Urls = new List<string>();
        }
    }
}
