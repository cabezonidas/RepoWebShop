using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public interface IEmailRepository
    {
        void Send(Order order);
    }
}
