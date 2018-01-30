using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RepoWebShop.Interfaces;

namespace RepoWebShop.Models
{
    public class ShoppingCart
    {
        private readonly ISession _session;
        private readonly AppDbContext _appDbContext;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ICalendarRepository _calendarRepository;

        private ShoppingCart(AppDbContext appDbContext, ICalendarRepository calendarRepository, ISession session, string cartId, IShoppingCartRepository shoppingCartRepository)
        {
            _appDbContext = appDbContext;
            _session = session;
            shoppingCartId = cartId;
            _shoppingCartRepository = shoppingCartRepository;
            _calendarRepository = calendarRepository;
        }

        private string shoppingCartId;

        public string GetShoppingCartId()
        {
            var payments = _appDbContext.PaymentNotices.Where(x => x.BookingId == shoppingCartId).ToList();
            
            if (payments.Count() > 0)
            {
                var newBookingId = GetRandomBookingId();
                _session.SetString("CartId", newBookingId);
                SetShoppingCartId(newBookingId);
            }

            return shoppingCartId;
        }

        public void SetShoppingCartId(string value)
        {
            shoppingCartId = value;
        }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public string GetShoppingCartComments()
        {
            return _shoppingCartRepository.GetComments(shoppingCartId)?.Comments;
        }

        public int GetShoppingCartPreparationTime()
        {
            var items = GetShoppingCartItems();
            return items.Count == 0 ? 0 : items.OrderByDescending(x => x.Pie.PieDetail.PreparationTime).First().Pie.PieDetail.PreparationTime;
        }

        public void AddComments(string comments)
        {
            _appDbContext.ShoppingCartComments.Add(
                new ShoppingCartComment()
                {
                    ShoppingCartId = GetShoppingCartId(),
                    Comments = comments,
                    Created = _calendarRepository.LocalTime()
                }
            );
            _appDbContext.SaveChanges();
        }



        internal void ClearFromCart(int pieId)
        {
            var shoppingCartItem =
                _appDbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Pie.PieId == pieId && s.ShoppingCartId == GetShoppingCartId());

            _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);

            _appDbContext.SaveChanges();
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<AppDbContext>();
            var shoppingCartRepository = services.GetService<IShoppingCartRepository>();
            var calendarRepository = services.GetService<ICalendarRepository>();

            string cartId = session.GetString("CartId") ?? GetRandomBookingId();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context, calendarRepository, session, cartId, shoppingCartRepository);
        }

        private static string GetRandomBookingId()
        {
            return Guid.NewGuid().ToString("D").ToUpper();
        }

        public void AddToCart(Pie pie, int amount)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == GetShoppingCartId());

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = GetShoppingCartId(),
                    Pie = pie,
                    Amount = 1
                };

                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == GetShoppingCartId());

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }


        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems =
                       _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == GetShoppingCartId())
                           .Include(s => s.Pie)
                           .Include(s => s.Pie.PieDetail)
                           .ToList());
        }

        public void ClearCart()
        {
            var cartItems = _appDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == GetShoppingCartId());
            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);

            var cartComments = _appDbContext
                .ShoppingCartComments
                .Where(cart => cart.ShoppingCartId == GetShoppingCartId());
            _appDbContext.ShoppingCartComments.RemoveRange(cartComments);

            _appDbContext.SaveChanges();
        }



        public decimal GetShoppingCartTotal()
        {
            var total = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == GetShoppingCartId())
                .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }
    }
}
