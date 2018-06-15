using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml;

namespace RepoWebShop.Interfaces
{
    public interface IPrinterRepository
    {
        void AddToQueue(string printData);
        IEnumerable<PrintQueue> GetQueue();
        void SubmitQueue(IEnumerable<PrintQueue> printjobs);
        void ClearQueue();
        Task AddToQueueAsync(int orderId);
    }
}
