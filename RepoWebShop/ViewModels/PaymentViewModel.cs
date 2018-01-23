using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.ViewModels
{
    public class PaymentViewModel
    {
        public PaymentNotice PaymentNotice { get; set; }
        public Order Order { get; set; }
    }
}
