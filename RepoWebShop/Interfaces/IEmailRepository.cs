using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IEmailRepository
    {
        Task SendOrderConfirmationAsync(Order order, Action notifyAdmins);
        Task SendEmailActivationAsync(ApplicationUser appUser);
        Task NotifyOrderCompleteAsync(Order order);
        Task SendEmailResetPasswordAsync(ApplicationUser foundUser);
        Task SendPromoAsync(string subject, string emailBody, IEnumerable<string> allEmails);
    }
}
