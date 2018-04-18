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
                return _shoppingCartId;
            }
            private set
            {
                _shoppingCartId = value;
            }
        }

        public void RenewId()
        {
            _shoppingCartId = Guid.NewGuid().ToString("D").ToUpper();
            _ctx.Session.SetString("CartId", _shoppingCartId);
        }

        private void InitializeId()
        {
            _shoppingCartId = _ctx.Session.GetString("CartId") ?? Guid.NewGuid().ToString("D").ToUpper();
            _ctx.Session.SetString("CartId", _shoppingCartId);
        }
    }
}
