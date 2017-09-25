using RepoWebShop.Models;
namespace RepoWebShop.Interfaces
{
    public interface IShoppingCartRepository
    {
        Order CreateOrderByPayment(PaymentNotice paymentNotice);
        string GetComments(string shoppingCartId);
    }
}
