using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderReturned : IOrderProgressState
    {
        public IOrderProgressState Cancel(Action saveProgressChanges) => this;

        public IOrderProgressState Complete(Action saveProgressChanges) => this;

        public IOrderProgressState PickUp(Action saveProgressChanges) => this;

        public IOrderProgressState Return(Action saveProgressChanges) => this;
    }
}
