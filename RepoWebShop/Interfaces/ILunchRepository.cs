using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface ILunchRepository
    {
        Task<Lunch> GetLunchByIdAsync(int lunchId);
        Task<LunchItem> AddItemInstanceAsync(int lunchId, int productId);
        Task<LunchItem> AddItemAsync(int lunchId, int productId);
        Task<LunchItem> RemoveItemInstanceAsync(int lunchId, int productId);
        Task<LunchItem> RemoveItemAsync(int lunchId, int productId);
        Task<LunchMiscellaneous> AddMiscellaneousAsync(int lunchId, string description, decimal price);
        Task<LunchMiscellaneous> AddMiscellaneousInstanceAsync(int lunchId, int miscellaneousId);
        Task<LunchMiscellaneous> RemoveMiscellaneousInstanceAsync(int lunchId, int miscellaneousId);
        Task RemoveMiscellaneousAsync(int lunchId, int miscellaneousId);
        Task<IEnumerable<Lunch>> GetAllLunchesAsync(Func<Lunch, bool> condition = null);
        Task CopyLunchAsync(int id);
        Task ModifyLunchAsync(int id);
        int SaveLunch(string bookingId = null);
        int GetBites(Lunch lunch);
        int GetConvitees(Lunch lunch);
        decimal GetTotal(Lunch lunch);
        decimal GetLunchTotalInStore(Lunch lunch);
        LunchMiscellaneous GetMiscellaneous(int id);
    }
}