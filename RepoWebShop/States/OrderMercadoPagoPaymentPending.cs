using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderMercadoPagoPaymentPending : IOrderPaymentStatus
    {
        public IOrderPaymentStatus Cancel(Action savePaymentChanges, Action mercadoPagoCancel)
        {
            savePaymentChanges();
            mercadoPagoCancel();
            return new OrderMercadoPagoNotPaid();
        }

        public IOrderPaymentStatus Pay(Action savePaymentChanges)
        {
            savePaymentChanges();
            return new OrderMercadoPagoPaid();
        }

        public IOrderPaymentStatus Refund(Action savePaymentChanges, Action mercadoPagoRefund) => this;
    }
}
