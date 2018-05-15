using System.Collections.Generic;
using RepoWebShop.Models;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IPieRepository
    {
        IEnumerable<Pie> ActivePies { get; }
        IEnumerable<Pie> AllPies { get; } 
        IEnumerable<PieDetail> PiesOfTheWeek { get; }
        Pie GetPieById(int pieId);
        Pie Add(Pie pie);
        void Delete(int pieId);
        void Restore(int pieId);
        void UpdatePrice(int pieId, int price);
        Task<int> Update(Pie pie);
        void SavePrice(int pieId, decimal onlinePrice, decimal storePrice);
    }
}
