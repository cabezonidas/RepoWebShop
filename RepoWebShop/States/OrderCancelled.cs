using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderCancelled : IOrderProgressState
    {
        public IOrderProgressState Cancel(Action savePaymentChanges) => this;

        public IOrderProgressState Complete(Action savePaymentChanges, Action notifyCustomer)
        {
            savePaymentChanges();
            notifyCustomer();
            return new OrderComplete();
        }

        public IOrderProgressState PickUp(Action savePaymentChanges)
        {
            savePaymentChanges();
            return new OrderPickedUp();
        }

        public IOrderProgressState Return(Action saveProgressChanges) => this;
    }
}
