using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IOrderProgressState
    {
        IOrderProgressState Cancel(Action saveProgressChanges);
        IOrderProgressState Complete(Action saveProgressChanges);
        IOrderProgressState PickUp(Action saveProgressChanges);
        IOrderProgressState Return(Action saveProgressChanges);
    }
}
