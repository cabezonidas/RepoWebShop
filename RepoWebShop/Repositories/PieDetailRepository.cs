using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RepoWebShop.Models;
using RepoWebShop.Interfaces;

namespace RepoWebShop.Repositories
{
    public class PieDetailRepository : IPieDetailRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IPieRepository _pieRepository;

        public PieDetailRepository(IPieRepository pieRepository, IMapper mapper, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _pieRepository = pieRepository;
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

        public IEnumerable<PieDetail> PieDetailsWithChildren
        {
            get
            {
                var pieDetailsWithReferences = _pieRepository.ActivePies.Select(x => x.PieDetailId).Distinct();
                return PieDetails.Where(x => pieDetailsWithReferences.Contains(x.PieDetailId));
            }
            
        }

        public void Delete(int pieDetailId)
        {
            PieDetail pieDetail = _appDbContext.PieDetails.FirstOrDefault(x => x.PieDetailId == pieDetailId);
            _appDbContext.PieDetails.Remove(pieDetail);
            _appDbContext.SaveChanges();
        }


        public Task<int> Add(PieDetail pieDetail)
        {
            _appDbContext.PieDetails.Add(pieDetail);
            return _appDbContext.SaveChangesAsync();
        }

        public Task<int> Update(PieDetail pieDetail)
        {
            PieDetail oldPieDetail = _appDbContext.PieDetails.First(x => x.PieDetailId == pieDetail.PieDetailId);

            _mapper.Map(pieDetail, oldPieDetail);

            return _appDbContext.SaveChangesAsync();
        }

        public PieDetail GetPieDetailById(int pieDetailId)
        {
            return _appDbContext.PieDetails.FirstOrDefault(p => p.PieDetailId == pieDetailId);
        }
    }
}
