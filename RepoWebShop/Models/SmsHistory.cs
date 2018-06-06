using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class SmsHistory
    {
        public int SmsHistoryId { get; set; }
        public string Body { get; set; }
        public string Destintation { get; set; }
        public DateTime Date { get; set; }
    }
}
