using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace RepoWebShop.Models
{
    public class ShoppingCart
    {
        private readonly IServiceProvider _services;
        private ShoppingCart(string cartId, IServiceProvider services)
        {
            ShoppingCartId = cartId;
            _services = services;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString("D").ToUpper();
            session.SetString("CartId", cartId);

            return new ShoppingCart(cartId, services);
        }

        public string ShoppingCartId { get; private set; }

        public void RenewId()
        {
            ISession session = _services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            string cartId = Guid.NewGuid().ToString("D").ToUpper();
            session.SetString("CartId", cartId);
            ShoppingCartId = cartId;
        }
    }
}
