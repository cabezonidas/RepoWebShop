using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderMercadoPagoNotPaid : IOrderPaymentStatus
    {
        public IOrderPaymentStatus Cancel(Action savePaymentChanges, Action mercadoPagoCancel) => this;

        public IOrderPaymentStatus Pay(Action savePaymentChanges) => this;

        public IOrderPaymentStatus Refund(Action savePaymentChanges, Action mercadoPagoRefund) => this;
    }
}
