using RepoWebShop.Models;
using System.Collections.Generic;

namespace RepoWebShop.Interfaces
{
    public interface IDiscountRepository
    {
        IEnumerable<Discount> GetActives();
        Discount FindByCode(string code);
        void Add(Discount discount);
        void Delete(int id);
    }
}
