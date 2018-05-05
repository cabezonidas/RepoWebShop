using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class AdminNotification
    {
        public int AdminNotificationId { get; set; }
        public string Phone { get; set; }
        public string AdminUser { get; set; }
    }
}
