using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderMercadoPagoPaid : IOrderPaymentStatus
    {
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IOrderPaymentStatus> Cancel(Action savePaymentChanges, Func<Task> mercadoPagoCancel) => this;
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        public IOrderPaymentStatus Pay(Action savePaymentChanges) => this;

        public async Task<IOrderPaymentStatus> Refund(Action savePaymentChanges, Func<Task> mercadoPagoRefund)
        {
            savePaymentChanges();
            await mercadoPagoRefund();
            return new OrderMercadoPagoNotPaid();
        }
    }
}
