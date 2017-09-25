using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RepoWebShop.Models;
using RepoWebShop.Interfaces;

namespace RepoWebShop.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ShoppingCartRepository(IMapper mapper, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        private ShoppingCartComment GetShoppingCartComments(string bookingId)
        {
            return _appDbContext.ShoppingCartComments.FirstOrDefault(x => x.ShoppingCartId == bookingId);
        }

        private IQueryable<ShoppingCartItem> GetShoppingCartItems(string bookingId)
        {
            return _appDbContext.ShoppingCartItems.Where(x => x.ShoppingCartId == bookingId).Include(x => x.Pie).ThenInclude(x => x.PieDetail);
        }

        private void AddShoppingItemsToOrder(Order order)
        {
            var shoppingCartItems = GetShoppingCartItems(order.BookingId);

            var preparationTime = 0;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    OrderId = order.OrderId,
                    Price = shoppingCartItem.Pie.Price
                };
                preparationTime = preparationTime > shoppingCartItem.Pie.PieDetail.PreparationTime ? preparationTime : shoppingCartItem.Pie.PieDetail.PreparationTime;
                _appDbContext.OrderDetails.Add(orderDetail);
            }

            order.PreparationTime = preparationTime;

            _appDbContext.ShoppingCartItems.RemoveRange(shoppingCartItems);

            _appDbContext.SaveChanges();
        }

        private void AddCommentsToOrder(Order order)
        {
            var customerComments = GetShoppingCartComments(order.BookingId);
            order.CustomerComments = customerComments?.Comments ?? String.Empty;
            if (customerComments != null)
                _appDbContext.ShoppingCartComments.Remove(customerComments);
            _appDbContext.SaveChanges();
        }

        public Order CreateOrderByPayment(PaymentNotice paymentNotice)
        {
            var order = _mapper.Map<PaymentNotice, Order>(paymentNotice);

            order.PickedUp = false;
            order.OrderPlaced = DateTime.Now;      
            
            //order.PreparationTime = _shoppingCart.GetShoppingCartPreparationTime();

            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();

            AddShoppingItemsToOrder(order);
            AddCommentsToOrder(order);

            return order;
        }

        public string GetComments(string shoppingCartId)
        {
            var shoppingCartComment = _appDbContext.ShoppingCartComments
                .Where(c => c.ShoppingCartId == shoppingCartId)
                .OrderByDescending(c => c.Created)
                .FirstOrDefault();

            return shoppingCartComment?.Comments;
        }
    }
}
