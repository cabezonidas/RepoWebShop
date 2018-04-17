using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RepoWebShop.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly AppDbContext _appDbContext;

        public DiscountRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(Discount discount)
        {
            if (FindByCode(discount.Phrase) == null)
            {
                _appDbContext.Discounts.Add(discount);
                _appDbContext.SaveChanges();
            }
            else
                throw new Exception("Código existente");
        }

        public void Delete(int id)
        {
            var discount = _appDbContext.Discounts.FirstOrDefault(x => x.DiscountId == id);
            if(discount != null)
            {
                discount.IsActive = false;
                _appDbContext.Discounts.Update(discount);
                _appDbContext.SaveChanges();
            }
        }

        public IEnumerable<Discount> GetActives() => _appDbContext.Discounts.Where(x => x.IsActive);

        public Discount FindByCode(string code) => GetActives().FirstOrDefault(x => x.Phrase.Trim().ToLower() == code.Trim().ToLower());
    }
}
