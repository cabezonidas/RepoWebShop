namespace RepoWebShop.Models
{
    public interface IShoppingCartRepository
    {
        Order CreateOrderByPayment(PaymentNotice paymentNotice);
        string GetComments(string shoppingCartId);
    }
}
