using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface ILunchRepository
    {
        int GetBites(Lunch lunch);
        int GetConvitees(Lunch lunch);
        decimal GetTotal(Lunch lunch);

        int SaveLunch(string bookingId = null);
        Lunch GetLunch(int id);

        LunchItem AddItemInstance(int lunchId, int productId);
        LunchItem RemoveItemInstance(int lunchId, int productId);
        LunchItem RemoveItem(int lunchId, int productId);
        LunchItem AddItem(int lunchId, int productId);

        LunchMiscellaneous AddMiscellaneous(int lunchId, string description, decimal price);
        IEnumerable<Lunch> GetAllLunches();
        LunchMiscellaneous AddMiscellaneousInstance(int lunchId, int miscellaneousId);
        LunchMiscellaneous RemoveMiscellaneousInstance(int lunchId, int miscellaneousId);
        void CopyLunch(int id);
        void ModifyLunch(int id);
        void RemoveMiscellaneous(int lunchId, int miscellaneousId);
        LunchMiscellaneous GetMiscellaneous(int id);
    }
}