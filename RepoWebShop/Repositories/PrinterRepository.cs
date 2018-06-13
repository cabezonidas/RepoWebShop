using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace RepoWebShop.Repositories
{
    public class PrinterRepository : IPrinterRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICalendarRepository _calendar;
        public PrinterRepository(AppDbContext appDbContext, ICalendarRepository calendar)
        {
            _appDbContext = appDbContext;
            _calendar = calendar;
        }

        public void AddToQueue(string printer, XmlDocument printData)
        {
            var printjob = new PrintQueue
            {
                Created = _calendar.LocalTime(),
                Message = printData.OuterXml,
                Printer = printer
            };

            _appDbContext.PrintQueue.Add(printjob);
            _appDbContext.SaveChanges();
        }

        public void AddToQueue(string printer, Order order, bool onlinePrice)
        {
            var printData = ConvertToPrintData(printer, order, onlinePrice);
            AddToQueue(printer, printData);
        }

        public void AddToQueue(string printer, Lunch lunch, bool onlinePrice)
        {
            var printData = ConvertToPrintData(printer, lunch, onlinePrice);
            AddToQueue(printer, printData);
        }

        public XmlDocument ConvertToPrintData(string printer, Order order, bool onlinePrice)
        {
            var body = title($"PEDIDO {order.FriendlyBookingId}") + header(order) +

            "<text>&#10;</text>" +
            "<text width=\"1\" height=\"1\"/>" +
            "<text reverse=\"false\" ul=\"false\" em=\"false\" color=\"color_1\"/>" +

            string.Concat(order.OrderLines.Select(x => itemDetail($"{x.Pie.PieDetail.Name} {x.Pie.Name}", x.Price, x.Amount))) +
            string.Concat(order.OrderCatalogItems.Select(x => itemDetail($"{x.Product.DisplayName}", onlinePrice ? x.Product.Price : x.Product.PriceInStore, x.Amount))) +


            "<text reverse=\"false\" ul=\"false\" em=\"true\"/>" +
            "<text width=\"2\" height=\"1\"/>" +
            "<text>TOTAL</text>" +
            "<text x=\"264\"/>" +
            "<text>    $12.00&#10;</text>" +
            "<text reverse=\"false\" ul=\"false\" em=\"false\"/>" +
            "<text width=\"1\" height=\"1\"/>" +
            "<feed unit=\"12\"/>" +
            "<text align=\"center\"/>" +
            "<feed line=\"3\"/>" +
            "<cut type=\"feed\"/>";

            var parameter = $"<Parameter><devid>{printer}</devid><timeout>10000</timeout></Parameter>";
            var printData = $"<PrintData><epos-print xmlns=\"http://www.epson-pos.com/schemas/2011/03/epos-print\">{body}</epos-print></PrintData>";
            var ePOSPrint = $"<ePOSPrint>{parameter}{printData}</ePOSPrint>";

            XmlDocument result = new XmlDocument();
            result.LoadXml(ePOSPrint);
            return result;
        }

        private static string title(string title) => "<text lang=\"en\"/>" +
            "<text smooth=\"true\"/>" +
            "<text align=\"center\"/>" +
            "<text font=\"font_b\"/>" +
            "<text width=\"2\" height=\"2\"/>" +
            "<text reverse=\"false\" ul=\"false\" em=\"true\" color=\"color_1\"/>" +
            $"<text>{title}&#10;</text>" +
            "<feed unit=\"12\"/>";

        private static string itemDetail(string name, decimal unitPrice, int quantity) => $"<text>{name}&#10;</text>" +
            $"<text>&#9;${unitPrice}  x  {quantity}</text>" +
            "<text x=\"384\"/>" +
            $"<text>    ${unitPrice * quantity}&#10;</text>" +
            "<text>&#10;</text>";

        private string header(Order order) => "<text>&#10;</text>" +
            "<text align=\"left\"/>" +
            "<text font=\"font_a\"/>" +
            "<text width=\"1\" height=\"1\"/>" +
            "<text reverse=\"false\" ul=\"false\" em=\"false\" color=\"color_1\"/>" +
            "<text>Pedido&#9;0001&#10;</text>" +
            "<text width=\"1\" height=\"1\"/>" +
            "<text reverse=\"false\" ul=\"false\" em=\"false\" color=\"color_1\"/>" +
            $"<text>Entrega&#9;{_calendar.FriendlyDate(order.PickUpTimeFrom)} {order.PickUpTimeFrom.Value.ToString("HH:ss")}&#10;</text>" +
            (!string.IsNullOrEmpty(order.CustomerComments) ?
                (
                    "<text>Notas&#10;</text>" +
                    "<text align=\"center\"/>" +
                    $"<text>{order.CustomerComments}&#10;</text>" +
                    "<text align=\"left\"/>"
                ) : "");

        public XmlDocument ConvertToPrintData(string printer, Lunch lunch, bool onlinePrice)
        {
            XmlDocument body = new XmlDocument();
            body.LoadXml(sample);
            return body;
        }

        public IEnumerable<PrintQueue> GetQueue()
        {
            return _appDbContext.PrintQueue.Where(x => !x.Printing && !x.Completed.HasValue).ToList();
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
            var printjobs = _appDbContext.PrintQueue.Where(x => x.Printing && !x.Completed.HasValue).ToList();
            var updates = printjobs.Select(x => {
                x.Printing = false;
                x.Completed = _calendar.LocalTime();
                return x;
            });
            _appDbContext.PrintQueue.UpdateRange(updates);
            _appDbContext.SaveChanges();
        }


        #region Sample Data

        private string sample =
              "<ePOSPrint>" +
                "<Parameter>" +
                  "<devid>local_printer</devid>" +
                  "<timeout>10000</timeout>" +
                "</Parameter>" +
                "<PrintData>" +
                  "<epos-print xmlns=\"http://www.epson-pos.com/schemas/2011/03/epos-print\">" +
                    "<text lang=\"en\"/>" +
                    "<text smooth=\"true\"/>" +
                    "<text align=\"center\"/>" +
                    "<text font=\"font_b\"/>" +
                    "<text width=\"2\" height=\"2\"/>" +
                    "<text reverse=\"false\" ul=\"false\" em=\"true\" color=\"color_1\"/>" +
                    "<text>DELIVERY TICKET&#10;</text>" +
                    "<feed unit=\"12\"/>" +
                    "<text>&#10;</text>" +
                    "<text align=\"left\"/>" +
                    "<text font=\"font_a\"/>" +
                    "<text width=\"1\" height=\"1\"/>" +
                    "<text reverse=\"false\" ul=\"false\" em=\"false\" color=\"color_1\"/>" +
                    "<text>Order&#9;0001&#10;</text>" +
                    "<text width=\"1\" height=\"1\"/>" +
                    "<text reverse=\"false\" ul=\"false\" em=\"false\" color=\"color_1\"/>" +
                    "<text>Time&#9;Mar 19 2013 13:53:15&#10;</text>" +
                    "<text>Seat&#9;A-3&#10;</text>" +
                    "<text>&#10;</text>" +
                    "<text width=\"1\" height=\"1\"/>" +
                    "<text reverse=\"false\" ul=\"false\" em=\"false\" color=\"color_1\"/>" +
                    "<text>Alt Beer&#10;</text>" +
                    "<text>&#9;$6.00  x  2</text>" +
                    "<text x=\"384\"/>" +
                    "<text>    $12.00&#10;</text>" +
                    "<text>&#10;</text>" +
                    "<text reverse=\"false\" ul=\"false\" em=\"true\"/>" +
                    "<text width=\"2\" height=\"1\"/>" +
                    "<text>TOTAL</text>" +
                    "<text x=\"264\"/>" +
                    "<text>    $12.00&#10;</text>" +
                    "<text reverse=\"false\" ul=\"false\" em=\"false\"/>" +
                    "<text width=\"1\" height=\"1\"/>" +
                    "<feed unit=\"12\"/>" +
                    "<text align=\"center\"/>" +
                    "<barcode type=\"code39\" hri=\"none\" font=\"font_a\" width=\"2\" height=\"60\">0001</barcode>" +
                    "<feed line=\"3\"/>" +
                    "<cut type=\"feed\"/>" +
                  "</epos-print>" +
                "</PrintData>" +
              "</ePOSPrint>";
        #endregion
    }
}
