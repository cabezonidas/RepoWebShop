using RepoWebShop.Models;
using System.Linq;

namespace RepoWebShop.Interfaces
{
    public interface IShoppingCartRepository
    {
        ShoppingCartComment GetComments(string bookingId);
        string ClearComments(string bookingId);
        IQueryable<ShoppingCartItem> GetItems(string bookingId);     
        IQueryable<ShoppingCartItem> EmptyItems(string bookingId);
    }
}
