using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class EmailMarketingHistory
    {
        public int EmailMarketingHistoryId { get; set; }
        public string Email { get; set; }
        public DateTime Sent { get; set; }
        public EmailMarketingTemplate EmailTemplate { get; set; }
    }
}
