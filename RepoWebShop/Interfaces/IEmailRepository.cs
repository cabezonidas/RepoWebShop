using RepoWebShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IEmailRepository
    {
        Task SendOrderConfirmationAsync(Order order, string hostUrl, PaymentNotice payment);
        Task SendEmailActivationAsync(ApplicationUser appUser, string hostUrl);
        Task NotifyOrderCompleteAsync(Order order, string hostUrl);
        Task SendEmailResetPasswordAsync(ApplicationUser foundUser, string hostUrl);
    }
}
