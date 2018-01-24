using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class OrderStatusViewModel
    {
        public string BookingId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public IOrderProgressState Progress { get; set; }
        public IOrderPaymentStatus Payment { get; set; }
        public EmailNotificationViewModel Notification { get; internal set; }
    }
}
