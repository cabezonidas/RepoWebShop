using RepoWebShop.Models;
using System.Collections.Generic;

namespace RepoWebShop.Interfaces
{
    public interface IDiscountRepository
    {
        IEnumerable<Discount> GetActives();
        Discount FindByCode(string code);
        Discount Add(Discount discount);
        void Delete(int id);
		bool IsValid(string code);
		Discount AddQuickDiscount(decimal value);
	}
}
