using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderPaymentNotKnown : IOrderPaymentStatus
    {
        public async Task<IOrderPaymentStatus> Cancel(Action savePaymentChanges, Func<Task> mercadoPagoCancel) => this;

        public IOrderPaymentStatus Pay(Action savePaymentChanges) => this;

        public async Task<IOrderPaymentStatus> Refund(Action savePaymentChanges, Func<Task> mercadoPagoRefund) => this;
    }
}
