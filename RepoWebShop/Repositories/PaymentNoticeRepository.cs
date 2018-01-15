﻿using RepoWebShop.Interfaces;
using RepoWebShop.Models;

namespace RepoWebShop.Repositories
{
    public class PaymentNoticeRepository : IPaymentNoticeRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IOrderRepository _orderRespository;
        private readonly IEmailRepository _emailRespository;
        private readonly ShoppingCart _shoppingCart;

        public PaymentNoticeRepository(AppDbContext appDbContext, IOrderRepository orderRepository, IEmailRepository emailRespository, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _orderRespository = orderRepository;
            _emailRespository = emailRespository;
            _shoppingCart = shoppingCart;
        }
        public void CreatePayment(PaymentNotice paymentNotification, string hostUrl)
        {
            _appDbContext.PaymentNotices.Add(paymentNotification);
            _appDbContext.SaveChanges();

            Order order = _orderRespository.CreateOrderByPayment(paymentNotification);
            _emailRespository.Send(order, hostUrl, paymentNotification);
        }
    }
}
