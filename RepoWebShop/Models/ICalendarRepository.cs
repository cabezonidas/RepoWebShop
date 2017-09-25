using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public interface ICalendarRepository
    {
        DateTime GetPickupEstimate(int hours);
    }
}
