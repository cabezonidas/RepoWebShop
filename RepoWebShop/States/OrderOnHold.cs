using RepoWebShop.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.States
{
    public class OrderOnHold : IOrderProgressState
    {
        public IOrderProgressState Cancel(Action saveProgressChanges) => this;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task<IOrderProgressState> Complete(Action saveProgressChanges, Func<Task> notifyCustomer) => this;
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

        public IOrderProgressState PickUp(Action saveProgressChanges) => this;

        public IOrderProgressState Return(Action saveProgressChanges) => this;
    }
}
