using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderMercadoPagoPaymentPending : IOrderPaymentStatus
    {
        public async Task<IOrderPaymentStatus> Cancel(Action savePaymentChanges, Func<Task> mercadoPagoCancel)
        {
            savePaymentChanges();
            await mercadoPagoCancel();
            return new OrderMercadoPagoNotPaid();
        }

        public IOrderPaymentStatus Pay(Action savePaymentChanges)
        {
            savePaymentChanges();
            return new OrderMercadoPagoPaid();
        }

        public async Task<IOrderPaymentStatus> Refund(Action savePaymentChanges, Func<Task> mercadoPagoRefund) => this;
    }
}
