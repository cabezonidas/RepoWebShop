using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderPickedUp : IOrderProgressState
    {
        public IOrderProgressState Cancel(Action savePaymentChanges) => this;

        public IOrderProgressState Complete(Action savePaymentChanges, Action notifyCustomer) => this;

        public IOrderProgressState PickUp(Action savePaymentChanges) => this;

        public IOrderProgressState Return(Action saveProgressChanges)
        {
            saveProgressChanges();
            return new OrderReturned();
        }
    }
}
