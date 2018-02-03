using RepoWebShop.Models;
using System.Collections.Generic;

namespace RepoWebShop.Interfaces
{
    public interface IEmailRepository
    {
        void SendOrderConfirmation(Order order, string hostUrl, PaymentNotice payment);
        void SendEmailActivationAsync(ApplicationUser appUser, string hostUrl);
        void NotifyOrderComplete(Order order, string hostUrl);
    }
}
