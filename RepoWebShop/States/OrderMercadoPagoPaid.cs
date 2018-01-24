using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderMercadoPagoPaid : IOrderPaymentStatus
    {
        public IOrderPaymentStatus Cancel(Action savePaymentChanges, Action mercadoPagoCancel) => this;

        public IOrderPaymentStatus Pay(Action savePaymentChanges) => this;

        public IOrderPaymentStatus Refund(Action savePaymentChanges, Action mercadoPagoRefund)
        {
            savePaymentChanges();
            mercadoPagoRefund();
            return new OrderMercadoPagoNotPaid();
        }
    }
}
