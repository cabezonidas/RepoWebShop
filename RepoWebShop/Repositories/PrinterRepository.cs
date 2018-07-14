using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RepoWebShop.Repositories
{
    public class PrinterRepository : IPrinterRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICalendarRepository _calendar;
        private readonly IHttpContextAccessor _ctx;
        public PrinterRepository(IHttpContextAccessor contextAccessor, AppDbContext appDbContext, ICalendarRepository calendar)
        {
            _ctx = contextAccessor;
            _appDbContext = appDbContext;
            _calendar = calendar;
        }

        public void AddToQueue(string printData)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(printData);

            var printjob = new PrintQueue
            {
                Created = _calendar.LocalTime(),
                Message = xml.OuterXml,
                Printer = "local_printer"
            };

            _appDbContext.PrintQueue.Add(printjob);
            _appDbContext.SaveChanges();
        }

        public XmlDocument ConvertToPrintData(Lunch lunch, bool onlinePrice)
        {
            XmlDocument body = new XmlDocument();
            body.LoadXml("");
            return body;
        }

        public IEnumerable<PrintQueue> GetQueue()
        {
            return _appDbContext.PrintQueue.Where(x => !x.Printing && !x.Completed.HasValue).ToArray();
        }

        public void SubmitQueue(IEnumerable<PrintQueue> printjobs)
        {
            var updates = printjobs.Select(x => {
                x.Printing = true;
                return x;
            });
            _appDbContext.PrintQueue.UpdateRange(updates);
            _appDbContext.SaveChanges();
        }

        public void ClearQueue()
        {
            var printjobs = _appDbContext.PrintQueue.Where(x => x.Printing && !x.Completed.HasValue).ToArray().AsEnumerable();
            var updates = printjobs.Select(x => {
                x.Printing = false;
                x.Completed = _calendar.LocalTime();
                return x;
            });
            _appDbContext.PrintQueue.UpdateRange(updates);
            _appDbContext.SaveChanges();
        }

        public async Task AddToQueueAsync(int orderId)
        {
            var host = _ctx.HttpContext.Request.HostUrl();
            var apicall = $"{host}/api/OrderData/PrintOnlineOrder/{orderId}/";
            HttpResponseMessage responseTask = await new HttpClient().PostAsync(apicall, new StringContent(string.Empty));
        }
    }
}
