using RepoWebShop.Models;

namespace RepoWebShop.Interfaces
{
    public interface IEmailRepository
    {
        void SendOrderConfirmation(Order order, string hostUrl, PaymentNotice payment);
        void SendEmailActivationAsync(ApplicationUser appUser, string hostUrl);
    }
}
