using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderReservationPaid : IOrderPaymentStatus
    {
        public IOrderPaymentStatus Pay(Action savePaymentChanges) => this;

        public IOrderPaymentStatus Refund(Action savePaymentChanges, Action mercadoPagoRefund)
        {
            savePaymentChanges();
            return new OrderReservationNotPaid();
        }
    }
}
