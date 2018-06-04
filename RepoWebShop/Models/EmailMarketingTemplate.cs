using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class EmailMarketingTemplate
    {
        public int EmailMarketingTemplateId { get; set; }
        public string EmailBody { get; set; }
        public DateTime Created { get; set; }
        public string Title { get; set; }
    }
}
