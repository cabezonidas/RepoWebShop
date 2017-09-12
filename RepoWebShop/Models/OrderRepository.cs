using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RepoWebShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;


        public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }
        
        public void UpdateManagementComments(int orderId, string comments)
        {
            _appDbContext.Orders.First(x => x.OrderId == orderId).ManagementComments = comments;
            _appDbContext.SaveChanges();
        }

        public void UpdatePickUpDate(int orderId, DateTime pickUp)
        {
            _appDbContext.Orders.First(x => x.OrderId == orderId).PickUp = pickUp;
            _appDbContext.SaveChanges();
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            _appDbContext.Orders.First(x => x.OrderId == orderId).Status = status;
            _appDbContext.SaveChanges();
        }

        public bool InvertPickedUpStatus(int orderId)
        {
            Order order = _appDbContext.Orders.First(x => x.OrderId == orderId);
            order.PickedUp = !order.PickedUp;
            _appDbContext.SaveChanges();
            return order.PickedUp;
        }

        public Order GetDraftOrderByBookingId(string bookingId)
        {
            return _appDbContext.Orders.Include(x => x.Registration).FirstOrDefault(x => x.BookingId == bookingId && x.Status == "draft");
        }

        public Order GetOrderByBookingId(string bookingId)
        {
            return _appDbContext.Orders.Include(x => x.Registration).FirstOrDefault(x => x.BookingId == bookingId);
        }

        public Order GetOrder(int id)
        {
            return _appDbContext.Orders.Include(x => x.Registration).FirstOrDefault(x => x.OrderId == id);
        }



        public IEnumerable<OrderDetail> GetOrderDetails(int id)
        {
            return _appDbContext.OrderDetails.Include(x => x.Pie).Where(x => x.OrderId == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _appDbContext.Orders.ToList();
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            order.PickedUp = false;

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            order.PreparationTime = _shoppingCart.GetShoppingCartPreparationTime();

            _appDbContext.Orders.Add(order);
            _appDbContext.SaveChanges();

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    OrderId = order.OrderId,
                    Price = shoppingCartItem.Pie.Price
                };

                _appDbContext.OrderDetails.Add(orderDetail);
            }
            _appDbContext.SaveChanges();
        }
    }
}
