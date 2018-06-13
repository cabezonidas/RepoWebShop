using RepoWebShop.Models;
using System.Collections.Generic;
using System.Xml;

namespace RepoWebShop.Interfaces
{
    public interface IPrinterRepository
    {
        void AddToQueue(string printer, XmlDocument printData);
        IEnumerable<PrintQueue> GetQueue();
        void SubmitQueue(IEnumerable<PrintQueue> printjobs);
        void ClearQueue();
        XmlDocument ConvertToPrintData(string printer, Order order, bool onlinePrice);
        XmlDocument ConvertToPrintData(string printer, Lunch lunch, bool onlinePrice);

        void AddToQueue(string printer, Order order, bool onlinePrice);
        void AddToQueue(string printer, Lunch lunch, bool onlinePrice);
    }
}
