using RepoWebShop.Models;

namespace RepoWebShop.Interfaces
{
    public interface IEmailRepository
    {
        void Send(Order order, string hostUrl, PaymentNotice payment);
    }
}
