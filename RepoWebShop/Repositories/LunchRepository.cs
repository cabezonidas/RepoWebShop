using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Repositories
{
    public class LunchRepository : ILunchRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly ICatalogRepository _catalog;
        private readonly IConfiguration _config;
        private readonly int _cateringMinPrepTime;

        public LunchRepository(ICatalogRepository catalog, IConfiguration config, IMapper mapper, AppDbContext appDbContext, IShoppingCartRepository cartRepository)
        {
            _config = config;
            _catalog = catalog;
            _mapper = mapper;
            _appDbContext = appDbContext;
            _cartRepository = cartRepository;
            _cateringMinPrepTime = _config.GetValue<int>("CateringDefaultPreparationTime");
        }

        public async Task<Lunch> GetLunchByIdAsync(int lunchId) => (await GetAllLunchesAsync(x => x.LunchId == lunchId)).FirstOrDefault();

        public async Task<LunchItem> AddItemInstanceAsync(int lunchId, int productId)
        {
            var lunch = await GetLunchByIdAsync(lunchId);
            var product = _catalog.GetById(productId);

            var shoppingCartLunchItem = lunch.Items?.FirstOrDefault(x => x.Product == product);

            if (shoppingCartLunchItem == null)
            {
                shoppingCartLunchItem = new LunchItem { Lunch = lunch, Product = product, Quantity = 1 };
                _appDbContext.LunchItems.Add(shoppingCartLunchItem);
            }
            else
            {
                shoppingCartLunchItem.Quantity += 1;
                _appDbContext.LunchItems.Update(shoppingCartLunchItem);
            }


            _appDbContext.SaveChanges();
            return shoppingCartLunchItem;
        }


        public async Task<LunchItem> AddItemAsync(int lunchId, int productId)
        {
            var lunch = await GetLunchByIdAsync(lunchId);
            var product = _catalog.GetById(productId);

            var shoppingCartLunchItem = lunch.Items?.FirstOrDefault(x => x.Product == product);

            if (shoppingCartLunchItem == null)
            {
                shoppingCartLunchItem = new LunchItem { Lunch = lunch, Product = product, Quantity = 1 };
                _appDbContext.LunchItems.Add(shoppingCartLunchItem);
                _appDbContext.SaveChanges();
            }
            
            return shoppingCartLunchItem;
        }

        public async Task<LunchItem> RemoveItemInstanceAsync(int lunchId, int productId)
        {
            LunchItem result = null;
            var lunch = await GetLunchByIdAsync(lunchId);
            var product = _catalog.GetById(productId);

            var shoppingCartLunchItem = lunch.Items.FirstOrDefault(x => x.Product == product);

            if (shoppingCartLunchItem != null)
            {
                if(shoppingCartLunchItem.Quantity > 1)
                {
                    shoppingCartLunchItem.Quantity -= 1;
                    _appDbContext.LunchItems.Update(shoppingCartLunchItem);
                    result = shoppingCartLunchItem;
                }
                else
                {
                    _appDbContext.LunchItems.Remove(shoppingCartLunchItem);
                }
                _appDbContext.SaveChanges();
            }
            
            return result;
        }

        public async Task<LunchItem> RemoveItemAsync(int lunchId, int productId)
        {
            LunchItem result = null;
            var lunch = await GetLunchByIdAsync(lunchId);
            var product = _catalog.GetById(productId);

            var shoppingCartLunchItem = lunch.Items.FirstOrDefault(x => x.Product == product);

            if (shoppingCartLunchItem != null)
            {
                _appDbContext.LunchItems.Remove(shoppingCartLunchItem);
                _appDbContext.SaveChanges();
            }

            return result;
        }

        public int SaveLunch(string bookingId = null)
        {
            var result = 0;
            ShoppingCartLunch cartLunch = _cartRepository.GetSessionLunch(bookingId);
            if(cartLunch != null)
            {
                result = cartLunch.Lunch.LunchId;
                _appDbContext.ShoppingCartCustomLunch.Remove(cartLunch);
                if(GetTotal(cartLunch.Lunch) == 0)
                {
                    _appDbContext.Lunch.Remove(cartLunch.Lunch);
                }
                _appDbContext.SaveChanges();
            }
            return result;
        }

        public async Task<LunchMiscellaneous> AddMiscellaneousAsync(int lunchId, string description, decimal price)
        {
            var lunch = await GetLunchByIdAsync(lunchId);
            var miscellaneous = new LunchMiscellaneous { Description = description, Price = price, Quantity = 1, Lunch = lunch };
            await _appDbContext.LunchMiscellanea.AddAsync(miscellaneous);
            await _appDbContext.SaveChangesAsync();
            return miscellaneous;
        }

        public async Task<LunchMiscellaneous> AddMiscellaneousInstanceAsync(int lunchId, int miscellaneousId)
        {
            var lunch = await GetLunchByIdAsync(lunchId);
            var result = lunch.Miscellanea.First(x => x.LunchMiscellaneousId == miscellaneousId);

            result.Quantity += 1;

            _appDbContext.LunchMiscellanea.Update(result);
            await _appDbContext.SaveChangesAsync();

            return result;
        }

        public async Task<LunchMiscellaneous> RemoveMiscellaneousInstanceAsync(int lunchId, int miscellaneousId)
        {
            var lunch = await GetLunchByIdAsync(lunchId);
            var result = lunch.Miscellanea.First(x => x.LunchMiscellaneousId == miscellaneousId);

            result.Quantity -= result.Quantity > 1 ? 1 : 0;
            _appDbContext.LunchMiscellanea.Update(result);
            await _appDbContext.SaveChangesAsync();
            return result;
        }

        public async Task RemoveMiscellaneousAsync(int lunchId, int miscellaneousId)
        {
            var lunch = await GetLunchByIdAsync(lunchId);
            var result = lunch.Miscellanea.First(x => x.LunchMiscellaneousId == miscellaneousId);
            _appDbContext.LunchMiscellanea.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }

        public int GetBites(Lunch lunch)
        {
            if (lunch == null || lunch.Items == null)
                return 0;
            return lunch.Items.Sum(x => x.Product.MinOrderAmount * x.Quantity);
        }

        public int GetConvitees(Lunch lunch)
        {
            if (lunch == null || lunch.Items == null)
                return 0;
            var result = lunch.Items.Sum(x => x.Product.MinOrderAmount * x.Quantity);
            return result / 10;
        }

        public decimal GetTotal(Lunch lunch) => _cartRepository.GetLunchTotal(lunch);

        public decimal GetLunchTotalInStore(Lunch lunch) => _cartRepository.GetLunchTotalInStore(lunch);

        public LunchMiscellaneous GetMiscellaneous(int id)
        {
            return _appDbContext.LunchMiscellanea.First(x => x.LunchMiscellaneousId == id);
        }

        public async Task<IEnumerable<Lunch>> GetAllLunchesAsync(Func<Lunch, bool> condition = null)
        {
            var result = await _appDbContext.Lunch
                .Where(x => condition == null || condition(x))
                .Include(x => x.Miscellanea)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.PieDetail)
                .ToArrayAsync();

            return result;
        }

        public async Task CopyLunchAsync(int id)
        {
            //SaveLunch();
            var lunch = await GetLunchByIdAsync(id);
            var newSessionLunch = _cartRepository.GetOrCreateSessionLunch();
            var items = lunch.Items.Select(x => new LunchItem { Lunch = newSessionLunch.Lunch, Product = x.Product, Quantity = x.Quantity });
            var miscellanea = lunch.Miscellanea.Select(x => new LunchMiscellaneous { Lunch = newSessionLunch.Lunch, Description = x.Description, Price = x.Price, Quantity = x.Quantity });
            _appDbContext.LunchItems.AddRange(items);
            _appDbContext.LunchMiscellanea.AddRange(miscellanea);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task ModifyLunchAsync(int id)
        {
            SaveLunch();
            var lunch = await GetLunchByIdAsync(id);
            var result = new ShoppingCartLunch
            {
                BookingId = _cartRepository.GetSessionCartId(),
                Lunch = lunch
            };
            _appDbContext.ShoppingCartCustomLunch.Add(result);
            _appDbContext.SaveChanges();
        }
    }
}
