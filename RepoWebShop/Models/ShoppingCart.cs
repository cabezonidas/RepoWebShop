using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using RepoWebShop.Interfaces;

namespace RepoWebShop.Models
{
    public class ShoppingCart
    {
        private readonly IServiceProvider _services;
        private readonly AppDbContext _appDbContext;
        private readonly ICalendarRepository _calendarRepository;
        private readonly HttpContext _ctx;
        private string _shoppingCartId;

        private ShoppingCart(IServiceProvider services)
        {
            _services = services;
            _ctx = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            _appDbContext = services.GetRequiredService<AppDbContext>();
            _calendarRepository = services.GetRequiredService<ICalendarRepository>();
            InitializeId();
        }

        public string MpPreferenceId
        {
            get => _ctx.Session.GetString("PreferenceId");
            set
            {
                _ctx.Session.SetString("PreferenceId", value);
                LinkMPIdBookingId(value);
            }
        }

        private void LinkMPIdBookingId(string value)
        {
            var shoppingCartData = _appDbContext.ShoppingCartData.FirstOrDefault(x => x.BookingId == BookingId);
            if (shoppingCartData == null)
            {
                _appDbContext.ShoppingCartData.Add(new ShoppingCartData
                {
                    BookingId = _shoppingCartId,
                    Created = _calendarRepository.LocalTime(),
                    LastUpdate = _calendarRepository.LocalTime(),
                    MercadoPagoPreferenceId = value
                });
            }
            else
            {
                shoppingCartData.MercadoPagoPreferenceId = value;
                shoppingCartData.LastUpdate = _calendarRepository.LocalTime();
                _appDbContext.ShoppingCartData.Update(shoppingCartData);
            }
            _appDbContext.SaveChanges();
        }

        public static ShoppingCart GetCart(IServiceProvider services) => new ShoppingCart(services);

        public string BookingId
        {
            get
            {
                var isIdObsolete = _appDbContext.Orders.Count(x => x.BookingId == _shoppingCartId) > 0;
                if (isIdObsolete)
                    RenewId();
                LogBooking(_shoppingCartId);
                return _shoppingCartId;
            }
            private set
            {
                _shoppingCartId = value;
            }
        }

        public void RenewId()
        {
            string remoteIp = _ctx?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;
            var existingBooking = _appDbContext.ShoppingCartByIp.First(x => x.Ip == remoteIp);

            _shoppingCartId = Guid.NewGuid().ToString("D").ToUpper();
            _ctx.Session.SetString("CartId", _shoppingCartId);

            existingBooking.BookingId = _shoppingCartId;
            _appDbContext.ShoppingCartByIp.Update(existingBooking);
            _appDbContext.SaveChanges();

            LogBooking(_shoppingCartId);
        }

        private void InitializeId()
        {
            string remoteIp = _ctx?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;
            var existingBooking = _appDbContext.ShoppingCartByIp.FirstOrDefault(x => x.Ip == remoteIp);
            if (existingBooking != null)
                _shoppingCartId = existingBooking.BookingId;
            else
            {
                _shoppingCartId = _ctx.Session.GetString("CartId") ?? Guid.NewGuid().ToString("D").ToUpper();
                var newBooking = new ShoppingCartByIp { BookingId = _shoppingCartId, Ip = remoteIp };
                _appDbContext.ShoppingCartByIp.Add(newBooking);
                _appDbContext.SaveChanges();
            }
            _ctx.Session.SetString("CartId", _shoppingCartId);
            LogBooking(_shoppingCartId);
        }

        private void LogBooking(string bookingId)
        {
            var bookingDetails = new BookingRecord
            {
                BookingId = bookingId,
                Ip = _ctx?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty,
                Created = _calendarRepository.LocalTime()
            };

            var detailCounts = _appDbContext.BookingRecords.Count(x => x.BookingId == bookingId && x.Ip == bookingDetails.Ip);
            if(detailCounts == 0)
            {
                _appDbContext.BookingRecords.Add(bookingDetails);
                _appDbContext.SaveChanges();
            }
        }
    }
}
