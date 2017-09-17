using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RepoWebShop.Models
{
    public class PieDetailRepository : IPieDetailRepository
    {
        private readonly AppDbContext _appDbContext;
        public PieDetailRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<PieDetail> PieDetails
        {
            get
            {
                return _appDbContext.PieDetails
                    .Include(p => p.Category);
            }
        }

        public IEnumerable<PieDetail> PiesOfTheWeek
        {
            get
            {
                return _appDbContext.PieDetails.Where(p => p.IsPieOfTheWeek);
            }
        }

        public Task<int> Add(PieDetail pieDetail)
        {
            _appDbContext.PieDetails.Add(pieDetail);
            return _appDbContext.SaveChangesAsync();
        }

        public PieDetail GetPieDetailById(int pieDetailId)
        {
            return _appDbContext.PieDetails.FirstOrDefault(p => p.PieDetailId == pieDetailId);
        }
    }
}
