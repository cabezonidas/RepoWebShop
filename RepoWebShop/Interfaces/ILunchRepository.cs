using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface ILunchRepository
    {
        ShoppingCartLunch GetLunch(string bookingId);
        decimal GetCost(string bookingId);
        int GetBites(string bookingId);
        void AddMiscellaneous(string bookingId, string desc, decimal price);
        void RemoveMiscellaneous(string bookingId);
        ShoppingCartLunchItem AddItemInstance(string bookingId, int productId);
        ShoppingCartLunchItem RemoveItemInstance(string bookingId, int productId);
        ShoppingCartLunchItem RemoveItem(string bookingId, int productId);
        ShoppingCartLunchItem AddItem(string bookingId, int productId);
    }
}
