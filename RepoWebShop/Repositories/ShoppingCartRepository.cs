using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RepoWebShop.Models;
using RepoWebShop.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace RepoWebShop.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ICalendarRepository _calendarRepository;
        private readonly ShoppingCart _shoppingCart;
        private readonly IConfiguration _config;
        private int _minimumArsForOrderDelivery;
        private int _maxArsForReservation;

        public ShoppingCartRepository(IConfiguration config, ShoppingCart shoppingCart, IMapper mapper, AppDbContext appDbContext, ICalendarRepository calendarRepository)
        {
            _config = config;
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
            _mapper = mapper;
            _calendarRepository = calendarRepository;

            _minimumArsForOrderDelivery = _config.GetValue<int>("MinimumArsForOrderDelivery");
            _maxArsForReservation = _config.GetValue<int>("MaxArsForReservation");
        }

        private ShoppingCartComment GetShoppingCartComments(string bookingId) =>
            _appDbContext.ShoppingCartComments.FirstOrDefault(x => x.ShoppingCartId == bookingId);

        public IQueryable<ShoppingCartItem> GetItems(string bookingId)=>
            _appDbContext.ShoppingCartItems.Where(x => x.ShoppingCartId == bookingId).Include(x => x.Pie).ThenInclude(x => x.PieDetail);

        public ShoppingCartComment GetComments(string shoppingCartId) =>
            _appDbContext.ShoppingCartComments.Where(c => c.ShoppingCartId == shoppingCartId).OrderByDescending(c => c.Created).FirstOrDefault();

        public string ClearComments(string bookingId)
        {
            var result = GetComments(bookingId);
            if(result != null)
            {
                var comments = _appDbContext.ShoppingCartComments.Where(x => x.ShoppingCartId == bookingId);
                _appDbContext.ShoppingCartComments.RemoveRange(comments);
            }
                
            return result?.Comments ?? String.Empty;
        }

        public IQueryable<ShoppingCartItem> EmptyItems(string bookingId)
        {
            var result = GetItems(bookingId);
            _appDbContext.ShoppingCartItems.RemoveRange(result);
            return result;
        }

        /////////////////////////////////////////////////////////////////////////////

        public string GetShoppingCartComments() => GetComments(_shoppingCart.ShoppingCartId)?.Comments;

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
                    ShoppingCartId = _shoppingCart.ShoppingCartId,
                    Comments = comments,
                    Created = _calendarRepository.LocalTime()
                }
            );
            _appDbContext.SaveChanges();
        }

        public void ClearFromCart(int pieId)
        {
            var shoppingCartItem =
                _appDbContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Pie.PieId == pieId && s.ShoppingCartId == _shoppingCart.ShoppingCartId);
            _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
            _appDbContext.SaveChanges();
        }

        public void AddToCart(Pie pie, int amount)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == _shoppingCart.ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = _shoppingCart.ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };
                _appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
                shoppingCartItem.Amount++;
            _appDbContext.SaveChanges();
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == _shoppingCart.ShoppingCartId);

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

        public List<ShoppingCartItem> GetShoppingCartItems() =>
            _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == _shoppingCart.ShoppingCartId).Include(s => s.Pie).Include(s => s.Pie.PieDetail).ToList();

        public void ClearCart()
        {
            var cartItems = _appDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == _shoppingCart.ShoppingCartId);
            _appDbContext.ShoppingCartItems.RemoveRange(cartItems);

            var cartComments = _appDbContext
                .ShoppingCartComments
                .Where(cart => cart.ShoppingCartId == _shoppingCart.ShoppingCartId);
            _appDbContext.ShoppingCartComments.RemoveRange(cartComments);

            _appDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal() =>
            _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == _shoppingCart.ShoppingCartId).Select(c => c.Pie.Price * c.Amount).Sum();

        public string GetShoppingCartId() => _shoppingCart.ShoppingCartId;

        public DeliveryAddress GetShoppingCartDeliveryAddress() =>
            GetShoppingCartTotal() >= _minimumArsForOrderDelivery ? _appDbContext.DeliveryAddresses.FirstOrDefault(x => x.ShoppingCartId == _shoppingCart.ShoppingCartId) : null;

        public DeliveryAddress GetDelivery(string bookingId) =>
            _appDbContext.DeliveryAddresses.FirstOrDefault(x => x.ShoppingCartId == bookingId);

        public void RenewId() => _shoppingCart.RenewId();

        public void RemoveDelivery()
        {
            var result = _appDbContext.DeliveryAddresses.Where(x => x.ShoppingCartId == GetShoppingCartId());
            if (result != null)
            {
                _appDbContext.DeliveryAddresses.RemoveRange(result);
                _appDbContext.SaveChanges();
            }
        }
    }
}
