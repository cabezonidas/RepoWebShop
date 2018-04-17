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
        private readonly IDiscountRepository _discountRepository;
        private int _minimumArsForOrderDelivery;
        private int _maxArsForReservation;

        public ShoppingCartRepository(IConfiguration config, IDiscountRepository discountRepository, ShoppingCart shoppingCart, IMapper mapper, AppDbContext appDbContext, ICalendarRepository calendarRepository)
        {
            _config = config;
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
            _mapper = mapper;
            _calendarRepository = calendarRepository;
            _discountRepository = discountRepository;

            _minimumArsForOrderDelivery = _config.GetValue<int>("MinimumArsForOrderDelivery");
            _maxArsForReservation = _config.GetValue<int>("MaxArsForReservation");
        }

        private ShoppingCartComment GetShoppingCartComments(string bookingId) =>
            _appDbContext.ShoppingCartComments.FirstOrDefault(x => x.ShoppingCartId == bookingId);

        public IEnumerable<ShoppingCartItem> GetItems(string bookingId)=>
            _appDbContext.ShoppingCartItems.Include(x => x.Pie).ThenInclude(x => x.PieDetail).Where(x => x.ShoppingCartId == bookingId).ToList();

        public ShoppingCartComment GetComments(string shoppingCartId) =>
            _appDbContext.ShoppingCartComments.Where(c => c.ShoppingCartId == shoppingCartId).OrderByDescending(c => c.Created).FirstOrDefault();

        public ShoppingCartDiscount GetDiscount(string shoppingCartId) =>
            _appDbContext.ShoppingCartDiscount.Include(x => x.Discount).FirstOrDefault(c => c.ShoppingCartId == shoppingCartId);

        public string ClearComments(string bookingId)
        {
            var result = GetComments(bookingId);
            if(result != null)
            {
                var comments = _appDbContext.ShoppingCartComments.Where(x => x.ShoppingCartId == bookingId);
                _appDbContext.ShoppingCartComments.RemoveRange(comments);
                _appDbContext.SaveChanges();
            }
                
            return result?.Comments ?? String.Empty;
        }

        public IEnumerable<ShoppingCartItem> EmptyItems(string bookingId)
        {
            var result = GetItems(bookingId);
            _appDbContext.ShoppingCartItems.RemoveRange(result);
            _appDbContext.SaveChanges();
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
            foreach (var bookingId in _appDbContext.Orders.Select(x => x.BookingId))
            {
                var cartItems = _appDbContext
                    .ShoppingCartItems
                    .Where(cart => cart.ShoppingCartId == bookingId);
                _appDbContext.ShoppingCartItems.RemoveRange(cartItems);

                var cartComments = _appDbContext
                    .ShoppingCartComments
                    .Where(cart => cart.ShoppingCartId == bookingId);
                _appDbContext.ShoppingCartComments.RemoveRange(cartComments);

                var cartDiscounts = _appDbContext
                    .ShoppingCartDiscount
                    .Include(x => x.Discount)
                    .Where(cart => cart.ShoppingCartId == bookingId);

                foreach (var discount in cartDiscounts)
                {
                    var disc = _appDbContext.Discounts.FirstOrDefault(x => x.DiscountId == discount.Discount.DiscountId);
                    if (disc != null)
                        if (disc.InstancesLeft.HasValue)
                        {
                            disc.InstancesLeft--;
                            _appDbContext.Discounts.Update(disc);
                            //Updetear preferenceId
                        }
                }

                _appDbContext.ShoppingCartDiscount.RemoveRange(cartDiscounts);
            }
            _appDbContext.SaveChanges();
        }

        public decimal GetShoppingCartItemsTotal() =>
            _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == _shoppingCart.ShoppingCartId).Select(c => c.Pie.Price * c.Amount).Sum();

        public decimal GetShoppingCartTotal()
        {
            var subtotal = GetShoppingCartTotalWithoutDiscount();
            var discount = GetShoppingDiscountAmount(subtotal);
            return subtotal + discount;
        }

        public string GetShoppingCartId() => _shoppingCart.ShoppingCartId;

        public DeliveryAddress GetShoppingCartDeliveryAddress() =>
            GetShoppingCartItemsTotal() >= _minimumArsForOrderDelivery ? _appDbContext.DeliveryAddresses.FirstOrDefault(x => x.ShoppingCartId == _shoppingCart.ShoppingCartId) : null;

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

        public string GetMpPreference() => _shoppingCart.MpPreferenceId;

        public void SetMpPreference(string preferenceId)
        {

            _shoppingCart.MpPreferenceId = preferenceId;
        }

        public Decimal GetShoppingDiscountAmount(decimal amount)
        {
            var cartDiscount = _appDbContext.ShoppingCartDiscount.Include(x => x.Discount).FirstOrDefault(x => x.ShoppingCartId == GetShoppingCartId());
            if(cartDiscount != null)
            {
                string error;
                return Discount.ApplyDiscount(_calendarRepository.LocalTime(), amount, cartDiscount.Discount, out error);
            }
            return 0;
        }

        private void AddShoppingDiscount(Discount discount)
        {
            _appDbContext.ShoppingCartDiscount.Add(
                new ShoppingCartDiscount
                {
                    Discount = discount,
                    ShoppingCartId = GetShoppingCartId()
                }
            );
            _appDbContext.SaveChanges();
        }

        public void RemoveShoppingDiscount()
        {
            _appDbContext.ShoppingCartDiscount.RemoveRange(_appDbContext.ShoppingCartDiscount.FirstOrDefault(x => x.ShoppingCartId == GetShoppingCartId()));
            _appDbContext.SaveChanges();
        }

        public Discount ClearDiscount(string bookingId)
        {
            var shoppingCartDiscount = GetDiscount(bookingId);
            Discount result = null;
            
            if (shoppingCartDiscount != null)
            {
                result = shoppingCartDiscount.Discount;
                var discounts = _appDbContext.ShoppingCartDiscount.Where(x => x.ShoppingCartId == bookingId);
                foreach(var discount in discounts)
                {
                    var disc = _appDbContext.Discounts.FirstOrDefault(x => x.DiscountId == discount.Discount.DiscountId);
                    if (disc != null)
                        if (disc.InstancesLeft.HasValue)
                        {
                            disc.InstancesLeft--;
                            _appDbContext.Discounts.Update(disc);
                            //Updatear preferenceId
                        }
                }
                _appDbContext.ShoppingCartDiscount.RemoveRange(discounts);
                _appDbContext.SaveChanges();
            }

            return result;
        }

        public Discount GetShoppingDiscount()
        {
            var result = GetDiscount(GetShoppingCartId());
            return result != null ? result.Discount : null;
        }

        public void AddDiscount(Discount discount)
        {
            var shoppingCartDiscount = GetDiscount(GetShoppingCartId());
            if(shoppingCartDiscount != null)
            {
                shoppingCartDiscount.Discount = discount;
                _appDbContext.ShoppingCartDiscount.Update(shoppingCartDiscount);
                _appDbContext.SaveChanges();
            }
            else
            {
                AddShoppingDiscount(discount);
            }
        }

        public decimal GetShoppingCartTotalWithoutDiscount()
        {
            var itemsCost = GetShoppingCartItemsTotal();
            var delivery = GetShoppingCartDeliveryAddress();
            var deliveryCost = (itemsCost >= _minimumArsForOrderDelivery && delivery != null) ? delivery.DeliveryCost : 0;
            var subtotal = itemsCost + deliveryCost;

            return subtotal;
        }
    }
}
