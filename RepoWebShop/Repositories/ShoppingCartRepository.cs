using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RepoWebShop.Models;
using RepoWebShop.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace RepoWebShop.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ICalendarRepository _calendarRepository;
        private readonly ShoppingCart _cartSession;
        private readonly IConfiguration _config;
        private readonly IDiscountRepository _discountRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private int _minimumArsForOrderDelivery;
        private int _maxArsForReservation;

        public ShoppingCartRepository(IHttpContextAccessor contextAccessor, IConfiguration config, IDiscountRepository discountRepository, ShoppingCart shoppingCart, IMapper mapper, AppDbContext appDbContext, ICalendarRepository calendarRepository)
        {
            _contextAccessor = contextAccessor;
            _config = config;
            _appDbContext = appDbContext;
            _cartSession = shoppingCart;
            _mapper = mapper;
            _calendarRepository = calendarRepository;
            _discountRepository = discountRepository;

            _minimumArsForOrderDelivery = _config.GetValue<int>("MinimumArsForOrderDelivery");
            _maxArsForReservation = _config.GetValue<int>("MaxArsForReservation");
        }

        public IEnumerable<ShoppingCartItem> GetItems(string bookingId = null)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            return _appDbContext.ShoppingCartItems.Include(x => x.Pie).ThenInclude(x => x.PieDetail).Where(x => x.ShoppingCartId == bookingId).ToList();
        }

        public ShoppingCartComment GetComments(string bookingId = null)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            return _appDbContext.ShoppingCartComments.Where(c => c.ShoppingCartId == bookingId).OrderByDescending(c => c.Created).FirstOrDefault();
        }

        public Discount GetDiscount(string bookingId = null)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            return _appDbContext.ShoppingCartDiscount.Include(x => x.Discount).FirstOrDefault(c => c.ShoppingCartId == bookingId)?.Discount;
        }

        public int GetPreparationTime(string bookingId = null)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var items = GetItems(bookingId);
            return items.Count() == 0 ? 0 : items.OrderByDescending(x => x.Pie.PieDetail.PreparationTime).First().Pie.PieDetail.PreparationTime;
        }

        public void ClearCart(string bookingId = null)
        {
            bookingId = bookingId ?? _cartSession.BookingId;

            _appDbContext.ShoppingCartItems.RemoveRange(_appDbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == bookingId));
            _appDbContext.ShoppingCartComments.RemoveRange(_appDbContext.ShoppingCartComments.Where(cart => cart.ShoppingCartId == bookingId));
            _appDbContext.ShoppingCartData.RemoveRange(_appDbContext.ShoppingCartData.Where(cart => cart.BookingId == bookingId));

            var cartDiscount = _appDbContext.ShoppingCartDiscount.Include(x => x.Discount).FirstOrDefault(cart => cart.ShoppingCartId == bookingId);
            if (cartDiscount != null)
            {
                cartDiscount.Discount.InstancesLeft--;
                _appDbContext.Discounts.Update(cartDiscount.Discount);
                _appDbContext.ShoppingCartDiscount.Remove(cartDiscount);
            }
            _appDbContext.SaveChanges();

            UpdateMPPreferencesWithDiscount(cartDiscount);

        }

        private void UpdateMPPreferencesWithDiscount(ShoppingCartDiscount cartDiscount)
        {
            if (cartDiscount != null)
            {
                var bookingsWithDiscountsInUse = _appDbContext.ShoppingCartDiscount.Where(x => x.Discount == cartDiscount.Discount).ToList();
                if (bookingsWithDiscountsInUse.Count > 0)
                {
                    var bookings = bookingsWithDiscountsInUse.Select(y => y.ShoppingCartId);
                    var currentPreferencesWithDiscount = _appDbContext.ShoppingCartData.Where(x => bookings.Contains(x.BookingId)).ToList();

                    if(currentPreferencesWithDiscount.Count > 0)
                    {
                        foreach(var pref in currentPreferencesWithDiscount)
                        {
                            Task.Run(async () =>
                            {
                                var apicall = $"http://{_contextAccessor.HttpContext.Request.Host.ToString()}/api/ShoppingCartData/GetMercadoPagoLink/{pref.BookingId}";
                                await new HttpClient().GetAsync(apicall);
                            });
                        }
                    }
                }
            }
        }

        public decimal GetItemsTotal(string bookingId = null)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            return _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == bookingId).Select(c => c.Pie.Price * c.Amount).Sum();
        }

        public decimal GetTotal(string bookingId = null)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var subtotal = GetTotalWithoutDiscount(bookingId);
            var discount = Discount.ApplyDiscount(_calendarRepository.LocalTime(), subtotal, GetDiscount(bookingId));
            return subtotal + discount;
        }

        public decimal GetTotalWithoutDiscount(string bookingId = null)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var items = GetItemsTotal(bookingId);
            var delivery = GetDelivery(bookingId)?.DeliveryCost ?? 0;
            return items + delivery;
        }

        public DeliveryAddress GetDelivery(string bookingId = null)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            return GetItemsTotal(bookingId) >= _minimumArsForOrderDelivery ? _appDbContext.DeliveryAddresses.FirstOrDefault(x => x.ShoppingCartId == bookingId) : null;
        }

        /////////////////////////////////////////////////////////////////////////////



        public void AddComments(string comments)
        {
            _appDbContext.ShoppingCartComments.RemoveRange(_appDbContext.ShoppingCartComments.Where(x => x.ShoppingCartId == _cartSession.BookingId));
            _appDbContext.ShoppingCartComments.Add(
                new ShoppingCartComment()
                {
                    ShoppingCartId = _cartSession.BookingId,
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
                    s => s.Pie.PieId == pieId && s.ShoppingCartId == _cartSession.BookingId);
            _appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
            _appDbContext.SaveChanges();
        }

        public void AddToCart(Pie pie, int amount)  
        {
            var shoppingCartItem =
                    _appDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == _cartSession.BookingId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = _cartSession.BookingId,
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
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == _cartSession.BookingId);

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

        public string GetSessionCartId() => _cartSession.BookingId;
       
        public void RenewId() => _cartSession.RenewId();

        public void RemoveDelivery()
        {
            var result = _appDbContext.DeliveryAddresses.Where(x => x.ShoppingCartId == GetSessionCartId());
            if (result != null)
            {
                _appDbContext.DeliveryAddresses.RemoveRange(result);
                _appDbContext.SaveChanges();
            }
        }

        public string GetMpPreference(string bookingId)
        {
            if(string.IsNullOrEmpty(bookingId))
                return _cartSession.MpPreferenceId;
            else
            {
                return _appDbContext.ShoppingCartData.FirstOrDefault(x => x.BookingId == bookingId)?.MercadoPagoPreferenceId ?? string.Empty;
            }
        }

        public void SetMpPreference(string preferenceId)
        {

            _cartSession.MpPreferenceId = preferenceId;
        }

        public void RemoveShoppingDiscount()
        {
            _appDbContext.ShoppingCartDiscount.RemoveRange(_appDbContext.ShoppingCartDiscount.FirstOrDefault(x => x.ShoppingCartId == GetSessionCartId()));
            _appDbContext.SaveChanges();
        }

        public void AddDiscount(Discount discount)
        {
            var shoppingCartDiscount = _appDbContext.ShoppingCartDiscount.FirstOrDefault(x => x.ShoppingCartId == _cartSession.BookingId);

            if(shoppingCartDiscount != null)
            {
                shoppingCartDiscount.Discount = discount;
                _appDbContext.ShoppingCartDiscount.Update(shoppingCartDiscount);
            }
            else
            {
                _appDbContext.ShoppingCartDiscount.Add(
                    new ShoppingCartDiscount
                    {
                        Discount = discount,
                        ShoppingCartId = _cartSession.BookingId
                    }
                );
            }
            _appDbContext.SaveChanges();
        }


    }
}
