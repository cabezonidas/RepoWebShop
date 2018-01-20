using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderReservationNotPaid : IOrderPaymentStatus
    {
        public IOrderPaymentStatus Pay(Action savePaymentChanges)
        {
            savePaymentChanges();
            return new OrderReservationPaid();
        }

        public IOrderPaymentStatus Refund(Action savePaymentChanges, Action mercadoPagoRefund) => this;
    }
}
