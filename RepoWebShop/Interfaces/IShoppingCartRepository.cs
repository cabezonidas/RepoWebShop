using RepoWebShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepoWebShop.Interfaces
{
    public interface IShoppingCartRepository
    {
        ShoppingCartComment GetComments(string bookingId);
        string ClearComments(string bookingId);
        IEnumerable<ShoppingCartItem> GetItems(string bookingId);     
        IEnumerable<ShoppingCartItem> EmptyItems(string bookingId);
        DeliveryAddress GetDelivery(string bookingId);

        string GetShoppingCartComments();
        int GetShoppingCartPreparationTime();
        void AddComments(string comments);
        void ClearFromCart(int pieId);
        void AddToCart(Pie pie, int amount);
        int RemoveFromCart(Pie pie);
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartItemsTotal();
        string GetShoppingCartId();
        DeliveryAddress GetShoppingCartDeliveryAddress();
        void RenewId();
        void RemoveDelivery();
        string GetMpPreference();
        void SetMpPreference(string preferenceId);
        decimal GetShoppingCartTotal();
        Discount GetShoppingDiscount();
        Discount ClearDiscount(string bookingId);
        void RemoveShoppingDiscount();
        void AddDiscount(Discount discount);
        decimal GetShoppingCartTotalWithoutDiscount();
    }
}
