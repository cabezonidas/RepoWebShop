using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    interface IDeliveryState
    {
        IDeliveryState AddDelivery(Action saveDeliveryChanges);
        IDeliveryState RemoveDelivery(Action saveDeliveryChanges);
        IDeliveryState GetDelivery(Action saveDeliveryChanges);
    }
}
