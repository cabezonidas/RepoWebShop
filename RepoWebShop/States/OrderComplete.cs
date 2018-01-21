using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderComplete : IOrderProgressState
    {
        public IOrderProgressState Cancel(Action savePaymentChanges)
        {
            savePaymentChanges();
            return new OrderCancelled();
        }

        public IOrderProgressState Complete(Action savePaymentChanges) => this;

        public IOrderProgressState PickUp(Action savePaymentChanges)
        {
            savePaymentChanges();
            return new OrderPickedUp();
        }

        public IOrderProgressState Return(Action saveProgressChanges) => this;
    }
}
