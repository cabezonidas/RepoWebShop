using Microsoft.EntityFrameworkCore;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepoWebShop.Repositories
{
    public class PieRepository: IPieRepository
    {
        private readonly AppDbContext _appDbContext;
        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pie> ActivePies
        {
            get
            {
                return _appDbContext.Pies
                    .Include(p => p.PieDetail)
                    .Include(c => c.PieDetail.Category)
                    .Where(x => x.IsActive);
            }
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _appDbContext.Pies
                    .Include(p => p.PieDetail)
                    .Include(c => c.PieDetail.Category);
            }
        }

        public IEnumerable<PieDetail> PiesOfTheWeek //To be removed. Must be replaced from IPieDetailRepository
        {
            get
            {
                return _appDbContext.PieDetails.Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie Add(Pie pie)
        {
            var result = _appDbContext.Pies.Add(pie);
            _appDbContext.SaveChanges();
            return pie;

        }

        public void Delete(int pieId)
        {
            Pie pie = _appDbContext.Pies.FirstOrDefault(x => x.PieId == pieId);
            pie.IsActive = false;
            _appDbContext.SaveChanges();
        }

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.Include(p => p.PieDetail).FirstOrDefault(p => p.PieId == pieId);
        }
    }
}
