using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IOrderPaymentStatus
    {
        Task<IOrderPaymentStatus> Refund(Action savePaymentChanges, Func<Task> mercadoPagoRefund);
        IOrderPaymentStatus Pay(Action savePaymentChanges);
		Task<IOrderPaymentStatus> Cancel(Action savePaymentChanges, Func<Task> mercadoPagoCancel);
    }
}
