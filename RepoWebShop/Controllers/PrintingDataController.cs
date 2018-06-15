using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class PrintingDataController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IPrinterRepository _printer;
        private readonly string printerKey;


        public PrintingDataController(IConfiguration config, IPrinterRepository printer)
        {
            _printer = printer;
            _config = config;
            printerKey = _config.GetSection("PrinterId").Value;
        }

        [HttpPost]
        [Route("Queue")]
        public IActionResult Queue()
        {

            var content = Request.BodyAsDictionary();

            if (content["ConnectionType"] == "GetRequest" && content["ID"] == printerKey)
            {
                IEnumerable<PrintQueue> queue = _printer.GetQueue();
                if(queue.Any())
                {
                    var printjob = $"<?xml version=\"1.0\" encoding=\"utf-8\"?><PrintRequestInfo>{string.Concat(queue.Select(x => x.Message))}</PrintRequestInfo>".Trim();
                    _printer.SubmitQueue(queue);
                    return Ok(printjob);
                }
            }

            if (content["ConnectionType"] == "SetResponse" && content["ID"] == printerKey)
            {
                XmlDocument responseFile = new XmlDocument();
                responseFile.LoadXml(content["ResponseFile"]);
                string success = responseFile.SelectSingleNode("PrintResponseInfo")?.FirstChild?.Attributes["success"]?.Value;

                switch (success)
                {
                    case "true":
                        _printer.ClearQueue();
                        break;
                    case "false":
                        var error = responseFile.SelectSingleNode("PrintResponseInfo").FirstChild.Attributes["code"].Value;
                        try
                        {
                            throw new Exception(error);
                        }
                        catch{ }
                        break;
                    default:
                        break;
                }
            }

            return Ok();
        }
    }
}
