using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class Unsubscribe
    {
        public string UnsubscribeId { get; set; }
        public string Email { get; set; }
        public DateTime Unsubscribed { get; set; }
    }
}
