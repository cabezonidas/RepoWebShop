using RepoWebShop.Models;

namespace RepoWebShop.Interfaces
{
    public interface IEmailRepository
    {
        void Send(Order order, PaymentNotice payment);
    }
}
