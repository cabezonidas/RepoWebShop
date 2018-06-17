using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using RepoWebShop.Models;
using RepoWebShop.Interfaces;
using RepoWebShop.ViewModels;
using RepoWebShop.Extensions;
using Microsoft.AspNetCore.Http;

namespace RepoWebShop.Repositories
{
    public class PieDetailRepository : IPieDetailRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IPieRepository _pieRepository;
        private readonly IFlickrRepository _flickrRepository;
        private readonly ICalendarRepository _calendar;
        private readonly IHttpContextAccessor _ctx;

        public PieDetailRepository(IHttpContextAccessor contextAccessor, ICalendarRepository calendarRepository, IFlickrRepository flicrRepository, IPieRepository pieRepository, IMapper mapper, AppDbContext appDbContext)
        {
            _calendar = calendarRepository;
            _flickrRepository = flicrRepository;
            _appDbContext = appDbContext;
            _mapper = mapper;
            _pieRepository = pieRepository;
            _ctx = contextAccessor;
        }

        public Dictionary<int, string> TimeEstimations(IEnumerable<Product> products)
        {
            var timeEstimations = new Dictionary<int, string>();
            foreach (var time in products.Select(x => x.PreparationTime).Distinct())
                timeEstimations.Add(time, _calendar.GetSoonestPickupEstimateForUsers(time));
            return timeEstimations;
        }

        public PieDetailViewModel MapDbPieDetailToPieDetailViewModel(PieDetail dbPieDetail)
        {
            var products = GetChildren(dbPieDetail.PieDetailId);
            var times = TimeEstimations(products);

            var result = new PieDetailViewModel()
            {
                IsMobile = _ctx.HttpContext.Request.IsMobile(),
                PrimaryPicture = _flickrRepository.GetAlbumPictures(dbPieDetail.FlickrAlbumId).PrimaryPicture,
                PieDetail = dbPieDetail,
                Pies = _pieRepository.ActivePies.Where(x => x.PieDetail.PieDetailId == dbPieDetail.PieDetailId)
            };

            result.Products = products
                .Select(x => _mapper.Map<Product, ProductEstimationViewModel>(x))
                .Select(x =>
                {
                    x.Estimation = times[x.PreparationTime];
                    return x;
                });

            return result;
        }

        public IEnumerable<PieDetail> PieDetails
        {
            get
            {
                return _appDbContext.PieDetails;
            }
        }

        public IEnumerable<PieDetail> PiesOfTheWeek
        {
            get
            {
                return PieDetailsWithChildren.Where(p => p.IsPieOfTheWeek);
            }
        }

        public IEnumerable<PieDetail> PieDetailsWithChildren
        {
            get
            {
                var pieDetailsWithReferences = _appDbContext.Products.Where(x => x.IsOnSale && x.IsActive).Include(x => x.PieDetail).Select(x => x.PieDetail.PieDetailId).Distinct();
                return PieDetails.Where(x => x.IsActive && x.FlickrAlbumId > 0 && pieDetailsWithReferences.Contains(x.PieDetailId));
            }
            
        }

        public void Delete(int pieDetailId)
        {
            PieDetail pieDetail = _appDbContext.PieDetails.FirstOrDefault(x => x.PieDetailId == pieDetailId);
            pieDetail.IsActive = false;
            //_appDbContext.PieDetails.Remove(pieDetail);
            _appDbContext.SaveChanges();
        }


        public async Task<int> Add(PieDetail pieDetail)
        {
            _appDbContext.PieDetails.Add(pieDetail);
            await _appDbContext.SaveChangesAsync();
            return pieDetail.PieDetailId;
        }

        public async Task<int> Update(PieDetail pieDetail)
        {
            PieDetail oldPieDetail = _appDbContext.PieDetails.First(x => x.PieDetailId == pieDetail.PieDetailId);
            _mapper.Map(pieDetail, oldPieDetail);
            await _appDbContext.SaveChangesAsync();
            return pieDetail.PieDetailId;
        }

        public PieDetail GetPieDetailById(int pieDetailId)
        {
            return _appDbContext.PieDetails.FirstOrDefault(p => p.PieDetailId == pieDetailId);
        }

        public void Restore(int pieDetailId)
        {
            var pieDetail = _appDbContext.PieDetails.FirstOrDefault(x => x.PieDetailId == pieDetailId);
            pieDetail.IsActive = true;
            _appDbContext.SaveChanges();
        }

        public void UpdateIngredients(ProductViewModel vm)
        {
            var result = _appDbContext.PieDetails.First(x => x.PieDetailId == vm.PieDetailId);
            result.Ingredients = vm.Ingredients.ToTitleCase();
            _appDbContext.PieDetails.Update(result);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Product> GetChildren(int id) => _appDbContext.Products.Include(x => x.PieDetail).Where(x => x.PieDetail.PieDetailId == id && x.IsActive && x.IsOnSale).ToList();
    }
}
