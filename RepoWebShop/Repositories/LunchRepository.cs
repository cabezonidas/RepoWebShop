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

        public ShoppingCartLunch GetSessionLunch(string bookingId = null)

        {
            bookingId = bookingId ?? _cartRepository.GetSessionCartId();
            ShoppingCartLunch result = _appDbContext.ShoppingCartLunch
                .Include(x => x.Lunch)
                .Include(x => x.Lunch.Miscellanea)
                .Include(x => x.Lunch.Items)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.BookingId == bookingId);

            if (result == null)
            {
                result = new ShoppingCartLunch
                {
                    BookingId = bookingId,
                    Lunch = new Lunch()
                };
                _appDbContext.ShoppingCartLunch.Add(result);
                _appDbContext.SaveChanges();
            }

            return result;
        }

        public Lunch GetLunch(int lunchId)
        {
            Lunch result = _appDbContext.Lunch
                .Include(x => x.Miscellanea)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
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

        public int SaveLunch()
        {
            ShoppingCartLunch lunch = GetSessionLunch();
            var result = lunch.Lunch.LunchId;
            _appDbContext.ShoppingCartLunch.Remove(lunch);
            _appDbContext.SaveChanges();
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

        public decimal GetTotal(Lunch lunch)
        {
            if (lunch == null || lunch.Items == null)
                return 0;
            var itemsPrice = lunch.Items.Sum(x => x.Product.MinOrderAmount * x.Quantity * x.Product.Price);
            var miscellaneaPrice = lunch.Miscellanea.Sum(x => x.Price * x.Quantity);
            return itemsPrice + miscellaneaPrice;
        }

        public LunchMiscellaneous GetMiscellaneous(int id)
        {
            return _appDbContext.LunchMiscellanea.First(x => x.LunchMiscellaneousId == id);
        }

        public IEnumerable<Lunch> GetAllLunches()
        {
            return _appDbContext.Lunch
                .Include(x => x.Miscellanea)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product).OrderByDescending(x => x.LunchId).ToList();
        }
    }
}
