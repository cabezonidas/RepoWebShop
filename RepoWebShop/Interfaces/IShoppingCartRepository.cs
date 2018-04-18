using RepoWebShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepoWebShop.Interfaces
{
    public interface IShoppingCartRepository
    {
        IEnumerable<ShoppingCartItem> GetItems(string bookingId = null);     
        ShoppingCartComment GetComments(string bookingId = null);
        DeliveryAddress GetDelivery(string bookingId = null);
        Discount GetDiscount(string bookingId = null);
        decimal GetItemsTotal(string bookingId = null);
        decimal GetTotalWithoutDiscount(string bookingId = null);
        int GetPreparationTime(string bookingId = null);
        void ClearCart(string bookingId = null);
        decimal GetTotal(string bookingId = null);

        void AddComments(string comments);
        void ClearFromCart(int pieId);
        void AddToCart(Pie pie, int amount);
        int RemoveFromCart(Pie pie);
        void RemoveDelivery();
        void RemoveShoppingDiscount();
        void AddDiscount(Discount discount);

        void RenewId();
        string GetSessionCartId();
        string GetMpPreference(string bookingId);
        void SetMpPreference(string preferenceId);
    }
}
