using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IOrderPaymentStatus
    {
        IOrderPaymentStatus Refund(Action savePaymentChanges, Action mercadoPagoRefund);
        IOrderPaymentStatus Pay(Action savePaymentChanges);
        IOrderPaymentStatus Cancel(Action savePaymentChanges, Action mercadoPagoCancel);
    }
}
