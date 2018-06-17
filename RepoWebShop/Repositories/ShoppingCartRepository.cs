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
using RepoWebShop.Extensions;

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
        private readonly int _minimumArsForOrderDelivery;
        private readonly int _maxArsForReservation;
        private readonly int _cateringMinPrepTime;

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
            _cateringMinPrepTime = _config.GetValue<int>("CateringDefaultPreparationTime");
        }

        public ShoppingCartLunch GetSessionLunchIfNotEmpty(string bookingId = null)
        {
            var lunchWithItems = GetSessionLunch(bookingId);
            if (GetLunchTotal(lunchWithItems?.Lunch) > 0)
                return lunchWithItems;
            else
                return null;
        }

        public ShoppingCartLunch GetSessionLunch(string bookingId = null)
        {
            bookingId = bookingId ?? GetSessionCartId();
            ShoppingCartLunch result = _appDbContext.ShoppingCartCustomLunch
                .Include(x => x.Lunch)
                .Include(x => x.Lunch.Miscellanea)
                .Include(x => x.Lunch.Items)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.PieDetail)
                .FirstOrDefault(x => x.BookingId == bookingId);
            return result;
        }
        public ShoppingCartLunch GetOrCreateSessionLunch(string bookingId = null)
        {
            bookingId = bookingId ?? GetSessionCartId();
            var result = GetSessionLunch(bookingId);

            if (result == null)
            {
                result = new ShoppingCartLunch
                {
                    BookingId = bookingId,
                    Lunch = new Lunch()
                    {
                        PreparationTime = _cateringMinPrepTime,
                    }
                };
                _appDbContext.ShoppingCartCustomLunch.Add(result);
                _appDbContext.SaveChanges();
            }

            return result;
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
            var catalogItems = GetCatalogItems(bookingId);
            var caterings = GetShoppingCaterings(bookingId);
            var customCatering = GetSessionLunchIfNotEmpty(bookingId);
            //var itemsPrepTime = items.Count() == 0 ? 0 : items.OrderByDescending(x => x.Pie.PieDetail.PreparationTime).First().Pie.PieDetail.PreparationTime;
            var cateringsPrepTime = caterings.Count() == 0 ? 0 : caterings.OrderByDescending(x => x.Lunch.PreparationTime).First().Lunch.PreparationTime;
            var catalogItemsPrepTime = catalogItems.Count() == 0 ? 0 : catalogItems.OrderByDescending(x => x.Product.PreparationTime).First().Product.PreparationTime;
            var customCateringPrepTime = customCatering?.Lunch?.PreparationTime ?? 0;
            var hours = new List<int>();
            hours.Add(0);
            //hours.Add(itemsPrepTime);
            hours.Add(cateringsPrepTime);
            hours.Add(catalogItemsPrepTime);
            hours.Add(customCateringPrepTime);
            return hours.OrderByDescending(x => x).First();
        }

        public IEnumerable<ShoppingCartCatalogItem> GetCatalogItems(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            return _appDbContext.ShoppingCartCatalogProducts.Include(x => x.Product).ThenInclude(x => x.PieDetail).Where(x => x.ShoppingCartId == bookingId).ToList();
        }

        public void ClearCart(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;

            _appDbContext.ShoppingCartPickUpDates.RemoveRange(_appDbContext.ShoppingCartPickUpDates.Where(cart => cart.BookingId == bookingId));
            _appDbContext.ShoppingCartItems.RemoveRange(_appDbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == bookingId));
            _appDbContext.ShoppingCartCatalogProducts.RemoveRange(_appDbContext.ShoppingCartCatalogProducts.Where(cart => cart.ShoppingCartId == bookingId));
            _appDbContext.ShoppingCartComments.RemoveRange(_appDbContext.ShoppingCartComments.Where(cart => cart.ShoppingCartId == bookingId));
            _appDbContext.ShoppingCartData.RemoveRange(_appDbContext.ShoppingCartData.Where(cart => cart.BookingId == bookingId));

            _appDbContext.ShoppingCartCaterings.RemoveRange(_appDbContext.ShoppingCartCaterings.Where(cart => cart.BookingId == bookingId));
            _appDbContext.ShoppingCartCustomLunch.RemoveRange(_appDbContext.ShoppingCartCustomLunch.Where(cart => cart.BookingId == bookingId));

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

                    if (currentPreferencesWithDiscount.Count > 0)
                    {
                        foreach (var pref in currentPreferencesWithDiscount)
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

        public decimal GetProductsTotal(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var items = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == bookingId)?.Select(c => c.Pie.Price * c.Amount)?.Sum() ?? 0;
            var catalogItems = _appDbContext.ShoppingCartCatalogProducts.Where(c => c.ShoppingCartId == bookingId)?.Select(c => c.Product.Price * c.Amount)?.Sum() ?? 0;
            var total = items + catalogItems;
            return total;
        }

        public decimal GetProductsTotalInStore(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var items = _appDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == bookingId)?.Select(c => c.Pie.Price * c.Amount)?.Sum() ?? 0;
            var catalogItems = _appDbContext.ShoppingCartCatalogProducts.Where(c => c.ShoppingCartId == bookingId)?.Select(c => c.Product.PriceInStore * c.Amount)?.Sum() ?? 0;
            var total = items + catalogItems;
            return total;
        }

        public decimal GetProductsOnlineSavings(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var result = GetProductsTotal(bookingId) - GetProductsTotalInStore(bookingId);
            result = result < 0 ? result : 0;
            return result;
        }

        public decimal GetTotal(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var subtotal = GetTotalWithoutDiscount(bookingId);
            string error;
            var discount = Discount.ApplyDiscount(_calendarRepository.LocalTime(), subtotal, GetDiscount(bookingId), out error);
            return subtotal + discount;
        }

        public decimal GetTotalWithoutDiscount(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var subtotal = GetSubtotalWithoutDelivery(bookingId);
            var delivery = GetDelivery(bookingId)?.DeliveryCost ?? 0;
            return delivery + subtotal;
        }

        public decimal GetTotalInStore(string bookingId) => GetTotalWithoutDiscountInStore(bookingId);

        public decimal GetTotalWithoutDiscountInStore(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var subtotal = GetSubtotalWithoutDeliveryInStore(bookingId);
            var delivery = GetDelivery(bookingId)?.DeliveryCost ?? 0;
            return delivery + subtotal;
        }

        public decimal GetSubtotalWithoutDelivery(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var items = GetProductsTotal(bookingId);
            var customLunch = GetLunchTotal(GetSessionLunch(bookingId)?.Lunch);
            var caterings = GetCateringsTotal(bookingId);
            return items + caterings + customLunch;
        }

        public decimal GetSubtotalWithoutDeliveryInStore(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var items = GetProductsTotalInStore(bookingId);
            var customLunch = GetLunchTotalInStore(GetSessionLunch(bookingId)?.Lunch);
            var caterings = GetCateringsTotalInStore(bookingId);
            return items + caterings + customLunch;
        }

        public decimal GetCateringsTotal(string bookingId)
        {
            var caterings = GetShoppingCaterings(bookingId);
            var total = caterings.Select(x => GetLunchTotal(x.Lunch) * x.Amount).Sum();
            return total;
        }

        public decimal GetLunchTotalInStore(Lunch lunch)
        {
            if (lunch == null || (lunch.Items == null && lunch.Miscellanea == null))
                return 0;
            var itemsPrice = lunch.Items.Sum(x => x.SubTotalInStore);
            var miscellaneaPrice = lunch.Miscellanea.Sum(x => x.Price * x.Quantity);
            return itemsPrice + miscellaneaPrice;
        }

        public decimal GetCateringsTotalSavings(string bookingId)
        {
            var result = GetCateringsTotal(bookingId) - GetCateringsTotal(bookingId);
            result = result < 0 ? result : 0;
            return result;
        }


        public decimal GetLunchTotal(Lunch lunch)
        {
            if (lunch == null || (lunch.Items == null && lunch.Miscellanea == null))
                return 0;
            var itemsPrice = lunch.Items.Sum(x => x.SubTotal);
            var miscellaneaPrice = lunch.Miscellanea.Sum(x => x.Price * x.Quantity);
            var total = itemsPrice + miscellaneaPrice;

            return total;
        }

        public decimal GetCateringsTotalInStore(string bookingId)
        {
            var caterings = GetShoppingCaterings(bookingId);
            var total = caterings.Select(x => GetLunchTotalInStore(x.Lunch) * x.Amount).Sum();
            return total;
        }


        public decimal GetLunchOnlineSavings(Lunch lunch)
        {
            var result = GetLunchTotal(lunch) - GetLunchTotalInStore(lunch);
            result = result < 0 ? result : 0;
            return result;
        }

        public DeliveryAddress GetDelivery(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            return GetSubtotalWithoutDelivery(bookingId) >= _minimumArsForOrderDelivery ? _appDbContext.DeliveryAddresses.FirstOrDefault(x => x.ShoppingCartId == bookingId) : null;
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
                _appDbContext.ShoppingCartItems.Where(
                    s => s.Pie.PieId == pieId && s.ShoppingCartId == _cartSession.BookingId);
            _appDbContext.ShoppingCartItems.RemoveRange(shoppingCartItem);
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
            var model = _calendarRepository.GetPickUpOption(prepTime, discount).Take(50);
            var selectedTime = GetPickUpDate(bookingId);
            var result = new PickUpTimeViewModel
            {
                TimeSlots = model.Where(x => !(x.Key == selectedTime.From && x.Value == selectedTime.To)),
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

        public SessionDetailsViewModel SessionsDetails(string friendlyBookingId = null)
        {
            var items = _appDbContext.ShoppingCartItems
                .Where(x => x.ShoppingCartId.ContainsSubstring(friendlyBookingId, true))
                .Include(x => x.Pie)
                .ThenInclude(x => x.PieDetail).ToList();
            var comments = _appDbContext.ShoppingCartComments
                .Where(x => x.ShoppingCartId.ContainsSubstring(friendlyBookingId, true))
                .Where(x => !String.IsNullOrEmpty(x.Comments)).ToList();
            var discounts = _appDbContext.ShoppingCartDiscount
                .Where(x => x.ShoppingCartId.ContainsSubstring(friendlyBookingId, true))
                .Include(x => x.Discount).ToList();
            var dates = _appDbContext.ShoppingCartPickUpDates
                .Where(x => x.BookingId.ContainsSubstring(friendlyBookingId, true))
                .ToList();
            var customLunches = _appDbContext.ShoppingCartCustomLunch
                .Where(x => x.BookingId.ContainsSubstring(friendlyBookingId, true))
                .Include(x => x.Lunch).ToList();
            var caterings = _appDbContext.ShoppingCartCaterings
                .Where(x => x.BookingId.ContainsSubstring(friendlyBookingId, true))
                .Include(x => x.Lunch).ToList();
            var products = _appDbContext.ShoppingCartCatalogProducts
                .Where(x => x.ShoppingCartId.ContainsSubstring(friendlyBookingId, true))
                .Include(x => x.Product)
                .ThenInclude(x => x.PieDetail).ToList();
            var delivery = _appDbContext.DeliveryAddresses
                .Where(x => x.ShoppingCartId.ContainsSubstring(friendlyBookingId, true)).ToList();
            var visits = _appDbContext.PageVisits
                .Where(x => x.BookingId.ContainsSubstring(friendlyBookingId, true))
                .OrderByDescending(x => x.Visited)
                .Include(x => x.User).ToList();
            var ips = _appDbContext.BookingRecords
                .Where(x => x.BookingId.ContainsSubstring(friendlyBookingId, true)).ToList();
            var repeatedIps = _appDbContext.BookingRecords
                .Where(x => ips.Select(y => y.Ip).Contains(x.Ip)).ToList();
            var orders = _appDbContext.Orders
                .Where(x => x.BookingId.ContainsSubstring(friendlyBookingId, true)).ToList();
            var users = visits.Where(x => x.User != null).Select(x => x.User).Distinct().ToList();

            SessionDetailsViewModel sessionDetails = new SessionDetailsViewModel()
            {
                Items = items,
                Comments = comments,
                Discounts = discounts,
                PickUpDates = dates,
                CustomLunches = customLunches,
                Caterings = caterings,
                Products = products,
                FriendlyBookingId = friendlyBookingId,
                Delivery = delivery,
                Visits = visits,
                Ips = ips.Union(repeatedIps).OrderByDescending(x => x.Created),
                Orders = orders,
                Users = users
            };
            return sessionDetails;
        }

        public void AddCatalogItemToCart(Product product, int v)
        {
            var shoppingCartProductItem =
                    _appDbContext.ShoppingCartCatalogProducts.Include(x => x.Product).FirstOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == _cartSession.BookingId);

            if (shoppingCartProductItem == null)
            {
                shoppingCartProductItem = new ShoppingCartCatalogItem
                {
                    ShoppingCartId = _cartSession.BookingId,
                    Product = product,
                    Created = _calendarRepository.LocalTime(),
                    Amount = v
                };
                _appDbContext.ShoppingCartCatalogProducts.Add(shoppingCartProductItem);
            }
            else
                shoppingCartProductItem.Amount = v + shoppingCartProductItem.Amount;
            _appDbContext.SaveChanges();
        }

        public int RemoveProductFromCart(Product product)
        {
            if (product == null)
                return 0;
            var shoppingCartCatalogItem =
                    _appDbContext.ShoppingCartCatalogProducts.FirstOrDefault(
                        s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == _cartSession.BookingId);

            var localAmount = 0;

            if (shoppingCartCatalogItem != null)
            {
                if (shoppingCartCatalogItem.Amount > 1)
                {
                    shoppingCartCatalogItem.Amount--;
                    localAmount = shoppingCartCatalogItem.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartCatalogProducts.Remove(shoppingCartCatalogItem);
                }
            }

            _appDbContext.SaveChanges();

            return localAmount;
        }

        public void ClearCatalogItemFromCart(int productId)
        {
            var shoppingCartCatalogItem =
                _appDbContext.ShoppingCartCatalogProducts.Where(
                    s => s.Product.ProductId == productId && s.ShoppingCartId == _cartSession.BookingId);
            _appDbContext.ShoppingCartCatalogProducts.RemoveRange(shoppingCartCatalogItem);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<ShoppingCartComboCatering> GetShoppingCaterings(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            var result = AllShoppingCartCaterings().Where(x => x.BookingId == bookingId).ToList();
            return result;
        }

        public void ClearCateringFromCart(int cateringId)
        {
            var shoppingCartCatering =
                AllShoppingCartCaterings().Where(
                    s => s.LunchId == cateringId && s.BookingId == _cartSession.BookingId);
            _appDbContext.ShoppingCartCaterings.RemoveRange(shoppingCartCatering);
            _appDbContext.SaveChanges();
        }

        public void RemoveLunchFromCart(Lunch lunch)
        {
            if (lunch == null)
                return;
            var shoppingCartCatering =
                    AllShoppingCartCaterings().FirstOrDefault(
                        s => s.Lunch == lunch && s.BookingId == _cartSession.BookingId);

            var localAmount = 0;

            if (shoppingCartCatering != null)
            {
                if (shoppingCartCatering.Amount > 1)
                {
                    shoppingCartCatering.Amount--;
                    localAmount = shoppingCartCatering.Amount;
                }
                else
                {
                    _appDbContext.ShoppingCartCaterings.Remove(shoppingCartCatering);
                }
            }
            _appDbContext.SaveChanges();
            return;
        }

        public void AddCateringToCart(Lunch catering, int amount)
        {
            var shoppingCartCatering = AllShoppingCartCaterings().FirstOrDefault(
                        s => s.Lunch == catering && s.BookingId == _cartSession.BookingId);

            if (shoppingCartCatering == null)
            {
                shoppingCartCatering = new ShoppingCartComboCatering
                {
                    BookingId = _cartSession.BookingId,
                    Lunch = catering,
                    LunchId = catering.LunchId,
                    Created = _calendarRepository.LocalTime(),
                    Amount = amount
                };
                _appDbContext.ShoppingCartCaterings.Add(shoppingCartCatering);
            }
            else
            {
                shoppingCartCatering.Amount = amount + shoppingCartCatering.Amount;
                _appDbContext.ShoppingCartCaterings.Update(shoppingCartCatering);
            }
            _appDbContext.SaveChanges();
        }

        public IEnumerable<ShoppingCartComboCatering> AllShoppingCartCaterings() => _appDbContext.ShoppingCartCaterings.Include(x => x.Lunch)
            .Include(x => x.Lunch.Miscellanea)
            .Include(x => x.Lunch.Items)
            .ThenInclude(x => x.Product)
            .ToList();

        public int CountTrolleyObjects(string bookingId)
        {
            bookingId = bookingId ?? _cartSession.BookingId;
            return GetItems(bookingId).Count() + GetCatalogItems(bookingId).Count() + GetShoppingCaterings(bookingId).Count() + (GetSessionLunchIfNotEmpty(bookingId) == null ? 0 : 1);
        }

        public void AddCateringToOrder(ShoppingCartLunch customLunch)
        {
            var result = new ShoppingCartComboCatering
            {
                BookingId = customLunch.BookingId,
                Lunch = customLunch.Lunch,
                LunchId = customLunch.Lunch.LunchId,
                Created = _calendarRepository.LocalTime(),
                Amount = 1
            };
            _appDbContext.ShoppingCartCaterings.Add(result);
            _appDbContext.SaveChanges();
        }

        public void ClearCustomCateringFromCart()
        {
            var bookingId = GetSessionCartId();
            _appDbContext.ShoppingCartCustomLunch.RemoveRange(_appDbContext.ShoppingCartCustomLunch.Where(x => x.BookingId == bookingId));
            _appDbContext.SaveChanges();
        }

        public IEnumerable<string> GetPendingBookings()
        {
            var result = new List<string>();
            result.AddRange(_appDbContext.ShoppingCartByIp.Select(x => x.BookingId));
            result.AddRange(_appDbContext.ShoppingCartCatalogProducts.Select(x => x.ShoppingCartId));
            result.AddRange(_appDbContext.ShoppingCartCaterings.Select(x => x.BookingId));
            result.AddRange(_appDbContext.ShoppingCartComments.Select(x => x.ShoppingCartId));
            result.AddRange(_appDbContext.ShoppingCartCustomLunch.Select(x => x.BookingId));
            result.AddRange(_appDbContext.ShoppingCartData.Select(x => x.BookingId));
            result.AddRange(_appDbContext.ShoppingCartDiscount.Select(x => x.ShoppingCartId));
            result.AddRange(_appDbContext.ShoppingCartItems.Select(x => x.ShoppingCartId));
            result.AddRange(_appDbContext.ShoppingCartPickUpDates.Select(x => x.BookingId));
            return result.Distinct().OrderBy(x => x).ToList();
        }
    }
}