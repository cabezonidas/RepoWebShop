using Microsoft.EntityFrameworkCore;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System.Linq;

namespace RepoWebShop.Repositories
{
    public class LunchRepository : ILunchRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IShoppingCartRepository _cartRepository;

        public LunchRepository(AppDbContext appDbContext, IShoppingCartRepository cartRepository)
        {
            _appDbContext = appDbContext;
            _cartRepository = cartRepository;
        }

        public ShoppingCartLunch GetLunch(string bookingId)
        {
            bookingId = bookingId ?? _cartRepository.GetSessionCartId();
            ShoppingCartLunch result = _appDbContext.ShoppingCartLunch
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .FirstOrDefault(x => x.BookingId == bookingId);

            if (result == null)
            {
                result = new ShoppingCartLunch { BookingId = bookingId };
                _appDbContext.ShoppingCartLunch.Add(result);
                _appDbContext.SaveChanges();
            }
            return result;
        }

        public decimal GetCost(string bookingId)
        {
            var lunch = GetLunch(bookingId);
            return lunch?.Items?.Sum(x => x.Product.MinOrderAmount * x.Product.Price * x.Quantity) ?? 0;
        }

        public int GetBites(string bookingId)
        {
            var lunch = GetLunch(bookingId);
            return lunch.Items?.Sum(x => x.Product.MinOrderAmount * x.Quantity) ?? 0;
        }

        public void AddMiscellaneous(string bookingId, string desc, decimal price)
        {
            var lunch = GetLunch(bookingId);
            lunch.MiscellaneousDescription = desc;
            lunch.MiscellaneousPrice = price;
            _appDbContext.ShoppingCartLunch.Update(lunch);
            _appDbContext.SaveChanges();
        }

        public void RemoveMiscellaneous(string bookingId)
        {
            AddMiscellaneous(bookingId, string.Empty, 0);
        }

        public ShoppingCartLunchItem AddItemInstance(string bookingId, int productId)
        {
            var lunch = GetLunch(bookingId);
            var product = _appDbContext.Products.First(x => x.ProductId == productId);

            var shoppingCartLunchItem = lunch.Items?.FirstOrDefault(x => x.Product == product);

            if (shoppingCartLunchItem == null)
            {
                shoppingCartLunchItem = new ShoppingCartLunchItem { ShoppingCartLunch = lunch, Product = product, Quantity = 1 };
                _appDbContext.ShoppingCartLunchItems.Add(shoppingCartLunchItem);
            }
            else
            {
                shoppingCartLunchItem.Quantity += 1;
                _appDbContext.ShoppingCartLunchItems.Update(shoppingCartLunchItem);
            }
            _appDbContext.SaveChanges();
            return shoppingCartLunchItem;
        }



        public ShoppingCartLunchItem AddItem(string bookingId, int productId)
        {
            var lunch = GetLunch(bookingId);
            var product = _appDbContext.Products.First(x => x.ProductId == productId);

            var shoppingCartLunchItem = lunch.Items?.FirstOrDefault(x => x.Product == product);

            if (shoppingCartLunchItem == null)
            {
                shoppingCartLunchItem = new ShoppingCartLunchItem { ShoppingCartLunch = lunch, Product = product, Quantity = 1 };
                _appDbContext.ShoppingCartLunchItems.Add(shoppingCartLunchItem);
                _appDbContext.SaveChanges();
            }
            return shoppingCartLunchItem;
        }

        public ShoppingCartLunchItem RemoveItemInstance(string bookingId, int productId)
        {
            ShoppingCartLunchItem result = null;
            var lunch = GetLunch(bookingId);
            var product = _appDbContext.Products.First(x => x.ProductId == productId);

            var shoppingCartLunchItem = lunch.Items.FirstOrDefault(x => x.Product == product);

            if (shoppingCartLunchItem != null)
            {
                if(shoppingCartLunchItem.Quantity > 1)
                {
                    shoppingCartLunchItem.Quantity -= 1;
                    _appDbContext.ShoppingCartLunchItems.Update(shoppingCartLunchItem);
                    result = shoppingCartLunchItem;
                }
                else
                {
                    _appDbContext.ShoppingCartLunchItems.Remove(shoppingCartLunchItem);
                }
                _appDbContext.SaveChanges();
            }
            return result;
        }

        public ShoppingCartLunchItem RemoveItem(string bookingId, int productId)
        {
            ShoppingCartLunchItem result = null;
            var lunch = GetLunch(bookingId);
            var product = _appDbContext.Products.First(x => x.ProductId == productId);

            var shoppingCartLunchItem = lunch.Items.FirstOrDefault(x => x.Product == product);

            if (shoppingCartLunchItem != null)
            {
                _appDbContext.ShoppingCartLunchItems.Remove(shoppingCartLunchItem);
                _appDbContext.SaveChanges();
            }
            return result;
        }
    }
}
