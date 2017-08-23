using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public class PieRepository: IPieRepository
    {
        private readonly AppDbContext _appDbContext;
        public PieRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Pie> Pies
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

        public Pie GetPieById(int pieId)
        {
            return _appDbContext.Pies.Include(p => p.PieDetail).FirstOrDefault(p => p.PieId == pieId);
        }
    }
}
