using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Collections.Generic;
using System.Linq;

namespace RepoWebShop.Repositories
{
    public class LunchRepository : ILunchRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IMapper _mapper;

        public LunchRepository(IMapper mapper, AppDbContext appDbContext, IShoppingCartRepository cartRepository)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _cartRepository = cartRepository;
        }

        public Lunch GetLunch(int lunchId)
        {
            Lunch result = _appDbContext.Lunch
                .Include(x => x.Miscellanea)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.PieDetail)
                .FirstOrDefault(x => x.LunchId == lunchId);
            
            return result;
        }

        public LunchItem AddItemInstance(int lunchId, int productId)
        {
            var lunch = GetLunch(lunchId);
            var product = _appDbContext.Products.First(x => x.ProductId == productId);

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



        public LunchItem AddItem(int lunchId, int productId)
        {
            var lunch = GetLunch(lunchId);
            var product = _appDbContext.Products.First(x => x.ProductId == productId);

            var shoppingCartLunchItem = lunch.Items?.FirstOrDefault(x => x.Product == product);

            if (shoppingCartLunchItem == null)
            {
                shoppingCartLunchItem = new LunchItem { Lunch = lunch, Product = product, Quantity = 1 };
                _appDbContext.LunchItems.Add(shoppingCartLunchItem);
                _appDbContext.SaveChanges();
            }
            return shoppingCartLunchItem;
        }

        public LunchItem RemoveItemInstance(int lunchId, int productId)
        {
            LunchItem result = null;
            var lunch = GetLunch(lunchId);
            var product = _appDbContext.Products.First(x => x.ProductId == productId);

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

        public LunchItem RemoveItem(int lunchId, int productId)
        {
            LunchItem result = null;
            var lunch = GetLunch(lunchId);
            var product = _appDbContext.Products.First(x => x.ProductId == productId);

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
            ShoppingCartLunch lunch = _cartRepository.GetSessionLunch(bookingId);
            if(lunch != null)
            {
                result = lunch.Lunch.LunchId;
                _appDbContext.ShoppingCartCustomLunch.Remove(lunch);
                if(GetTotal(lunch.Lunch) == 0)
                {
                    _appDbContext.Lunch.Remove(lunch.Lunch);
                }
                else
                {
                    if (lunch.Lunch.ComboPrice == 0)
                    {
                        lunch.Lunch.ComboPrice = GetTotal(lunch.Lunch);
                        _appDbContext.Lunch.Update(lunch.Lunch);
                    }
                }
                _appDbContext.SaveChanges();
            }
            return result;
        }

        public LunchMiscellaneous AddMiscellaneous(int lunchId, string description, decimal price)
        {
            var lunch = GetLunch(lunchId);
            var miscellaneous = new LunchMiscellaneous { Description = description, Price = price, Quantity = 1, Lunch = lunch };
            _appDbContext.LunchMiscellanea.Add(miscellaneous);
            _appDbContext.SaveChanges();
            return miscellaneous;
        }

        public LunchMiscellaneous AddMiscellaneousInstance(int lunchId, int miscellaneousId)
        {
            var lunch = GetLunch(lunchId);
            var result = lunch.Miscellanea.First(x => x.LunchMiscellaneousId == miscellaneousId);

            result.Quantity += 1;

            _appDbContext.LunchMiscellanea.Update(result);
            _appDbContext.SaveChanges();

            return result;
        }

        public LunchMiscellaneous RemoveMiscellaneousInstance(int lunchId, int miscellaneousId)
        {
            var lunch = GetLunch(lunchId);
            var result = lunch.Miscellanea.First(x => x.LunchMiscellaneousId == miscellaneousId);

            result.Quantity -= result.Quantity > 1 ? 1 : 0;
            _appDbContext.LunchMiscellanea.Update(result);
            _appDbContext.SaveChanges();
            return result;
        }

        public void RemoveMiscellaneous(int lunchId, int miscellaneousId)
        {
            var lunch = GetLunch(lunchId);
            var result = lunch.Miscellanea.First(x => x.LunchMiscellaneousId == miscellaneousId);
            _appDbContext.LunchMiscellanea.Remove(result);
            _appDbContext.SaveChanges();
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

        public LunchMiscellaneous GetMiscellaneous(int id)
        {
            return _appDbContext.LunchMiscellanea.First(x => x.LunchMiscellaneousId == id);
        }

        public IEnumerable<Lunch> GetAllLunches()
        {
            return _appDbContext.Lunch
                .Include(x => x.Miscellanea)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.PieDetail)
                .OrderByDescending(x => x.LunchId)
                .ToList().Where(x => GetTotal(x) > 0);
        }

        public void CopyLunch(int id)
        {
            //SaveLunch();
            var lunch = GetLunch(id);
            var newSessionLunch = _cartRepository.GetOrCreateSessionLunch();
            var items = lunch.Items.Select(x => new LunchItem { Lunch = newSessionLunch.Lunch, Product = x.Product, Quantity = x.Quantity }).ToList();
            var miscellanea = lunch.Miscellanea.Select(x => new LunchMiscellaneous { Lunch = newSessionLunch.Lunch, Description = x.Description, Price = x.Price, Quantity = x.Quantity }).ToList();
            _appDbContext.LunchItems.AddRange(items);
            _appDbContext.LunchMiscellanea.AddRange(miscellanea);
            _appDbContext.SaveChanges();
        }

        public void ModifyLunch(int id)
        {
            SaveLunch();
            var lunch = GetLunch(id);
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
