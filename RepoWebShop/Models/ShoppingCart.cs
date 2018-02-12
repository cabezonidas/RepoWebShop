using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace RepoWebShop.Models
{
    public class ShoppingCart
    {
        private readonly IServiceProvider _services;
        private readonly AppDbContext _appDbContext;
        private readonly ISession _session;
        private string _shoppingCartId;

        private ShoppingCart(IServiceProvider services)
        {
            _services = services;
            _session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            _appDbContext = services.GetRequiredService<AppDbContext>();
            InitializeId();
        }

        public static ShoppingCart GetCart(IServiceProvider services) => new ShoppingCart(services);

        public string ShoppingCartId
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
            _session.SetString("CartId", _shoppingCartId);
        }

        private void InitializeId()
        {
            _shoppingCartId = _session.GetString("CartId") ?? Guid.NewGuid().ToString("D").ToUpper();
            _session.SetString("CartId", _shoppingCartId);
        }
    }
}
