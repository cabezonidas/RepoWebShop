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
using RepoWebShop.ViewModels;

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

        public IEnumerable<ShoppingCartItem> GetItems(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            return _appDbContext.ShoppingCartItems.Include(x => x.Pie).ThenInclude(x => x.PieDetail).Where(x => x.ShoppingCartId == bookingId).ToList();
        }

        public ShoppingCartComment GetComments(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            return _appDbContext.ShoppingCartComments.Where(c => c.ShoppingCartId == bookingId).OrderByDescending(c => c.Created).FirstOrDefault();
        }

        public Discount GetDiscount(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            return _appDbContext.ShoppingCartDiscount.Include(x => x.Discount).FirstOrDefault(c => c.ShoppingCartId == bookingId)?.Discount;
        }

        public int GetPreparationTime(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var items = GetItems(bookingId);
            return items.Count() == 0 ? 0 : items.OrderByDescending(x => x.Pie.PieDetail.PreparationTime).First().Pie.PieDetail.PreparationTime;
        }

        public void ClearCart(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;

            _appDbContext.ShoppingCartPickUpDates.RemoveRange(_appDbContext.ShoppingCartPickUpDates.Where(cart => cart.BookingId == bookingId));
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

        public decimal GetItemsTotal(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            return _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == bookingId).Select(c => c.Pie.Price * c.Amount).Sum();
        }

        public decimal GetTotal(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var subtotal = GetTotalWithoutDiscount(bookingId);
            var discount = Discount.ApplyDiscount(_calendarRepository.LocalTime(), subtotal, GetDiscount(bookingId));
            return subtotal + discount;
        }

        public decimal GetTotalWithoutDiscount(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var items = GetItemsTotal(bookingId);
            var delivery = GetDelivery(bookingId)?.DeliveryCost ?? 0;
            return items + delivery;
        }

        public DeliveryAddress GetDelivery(string bookingId)
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

        public bool TrySetPickUpDate(string bookingId, DateTime pickUpDate, out string error)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            error = "";
            var preparationTime = GetPreparationTime(bookingId);
            var discount = GetDiscount(bookingId);
            var pickUpOptions = _calendarRepository.GetPickUpOption(preparationTime, discount);
            var validSelectedSlots = pickUpOptions.Where(x => x.Key <= pickUpDate && x.Key.Add(x.Value) >= pickUpDate);
            var validSlot = validSelectedSlots.Any();
            
            if(validSlot)
            {
                var pickUp = new ShoppingCartPickUpDate
                {
                    BookingId = bookingId,
                    From = validSelectedSlots.First().Key,
                    To = validSelectedSlots.First().Value,
                    UserSubmitted = true
                };
                _appDbContext.ShoppingCartPickUpDates.Add(pickUp);
                _appDbContext.SaveChanges();
                return true;
            }
            else
            {
                error = "La fecha seleccionada no es válida.";
                return false;
            }
        }

        public ShoppingCartPickUpDate GetPickUpDate(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var result = _appDbContext.ShoppingCartPickUpDates.LastOrDefault(x => x.BookingId == bookingId);
            var prepTime = GetPreparationTime(bookingId);
            var discount = GetDiscount(bookingId);
            var openSlots = _calendarRepository.GetPickUpOption(prepTime, discount);
            var defaultOption = openSlots.First();

            if (result == null)
                return InsertPickUpDate(bookingId, defaultOption, true, "");
            else
            {
                var validDate = WorkingHours.ContainsDateFrame(openSlots, new KeyValuePair<DateTime, TimeSpan>(result.From, result.To));
                if(!validDate)
                {
                    if(result.UserSubmitted)
                    {
                        var message = defaultOption.Key.Date == result.From.Date ?
                            "Se ajustó la hora de entrega." :
                            "La fecha que elegiste ya no es válida y fue cambiada.";

                        var userSubmitted = defaultOption.Key.Date == result.From.Date;
                        return InsertPickUpDate(bookingId, defaultOption, userSubmitted, message);
                    }
                    else
                    {
                        result.From = defaultOption.Key;
                        result.To = defaultOption.Value;
                        _appDbContext.ShoppingCartPickUpDates.Update(result);
                        _appDbContext.SaveChanges();
                        return result;
                    }
                }
            }
            return result;
        }

        private ShoppingCartPickUpDate InsertPickUpDate(string bookingId, KeyValuePair<DateTime, TimeSpan> defaultOption, bool userSubmitted, string message)
        {
            var pickUp = new ShoppingCartPickUpDate
            {
                BookingId = bookingId,
                From = defaultOption.Key,
                To = defaultOption.Value,
                UserSubmitted = userSubmitted,
                Message = message
            };
            _appDbContext.ShoppingCartPickUpDates.Add(pickUp);
            _appDbContext.SaveChanges();
            return pickUp;
        }

        public PickUpTimeViewModel GetTimeSlots(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var prepTime = GetPreparationTime(bookingId);
            var discount = GetDiscount(bookingId);
            var model = _calendarRepository.GetPickUpOption(prepTime, discount).Take(10);
            var selectedTime = GetPickUpDate(bookingId);
            var result = new PickUpTimeViewModel
            {
                TimeSlots = model.Where(x => x.Key != selectedTime.From && x.Value != selectedTime.To),
                SelectedTime = new KeyValuePair<DateTime, TimeSpan>(selectedTime.From, selectedTime.To),
                Message = selectedTime.Message,
                UserSubmitted = selectedTime.UserSubmitted
            };
            return result;
        }

        public void AcknowledgeSystemTime(string bookingId)
        {
            var pickUpDate = GetPickUpDate(bookingId);
            if (pickUpDate != null)
            {
                pickUpDate.UserSubmitted = true;
                pickUpDate.Message = string.Empty;
                _appDbContext.ShoppingCartPickUpDates.Update(pickUpDate);
                _appDbContext.SaveChanges();
            }
        }
    }
}