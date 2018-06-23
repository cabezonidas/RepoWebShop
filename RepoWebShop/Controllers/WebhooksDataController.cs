using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class WebhooksDataController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly IPaymentNoticeRepository _paymentNoticeRepository;
        private readonly IShoppingCartRepository _shoppingCart;
        private readonly AppDbContext _appDbContext;
        private readonly IMercadoPago _mp;
        private readonly IConfiguration _config;

        public WebhooksDataController(IHostingEnvironment env, IShoppingCartRepository shoppingCart, IConfiguration config, IPaymentNoticeRepository paymentNoticeRepository, IMercadoPago mp, AppDbContext appDbContext)
        {
            _env = env;
            _shoppingCart = shoppingCart;
            _config = config;
            _paymentNoticeRepository = paymentNoticeRepository;
            _mp = mp;
            _appDbContext = appDbContext;
        }

        [Route("OnPaymentNotNotified")]
        public async Task<IActionResult> OnPaymentNotNotified()
        {
            try
            {
                if(_env.IsProduction())
                    await CheckPayments();
                return Ok();
            }
            catch
            {
                return Ok();
            }
        }

        private async Task CheckPayments()
        {
            var payments = await _mp.SearchPaymentAsync(new Dictionary<string, string>(), 0, 0);
            var totalPayments = Int32.Parse(((payments["response"] as Hashtable)["paging"] as Hashtable)["total"].ToString());
            var chunckSize = 1000;
            for (int i = 0; i < totalPayments / chunckSize; i++)
                await CheckPaymentsChunck(totalPayments - ((i+1) * chunckSize), chunckSize);
            var left = totalPayments % chunckSize;
            if (left > 0)
                await CheckPaymentsChunck(0, left);
        }

        private async Task CheckPaymentsChunck(int offset, int limit)
        {
            var latestPayments = await LatestPayments(offset, limit);
            var pendingBookings = _shoppingCart.GetPendingBookings();
            foreach (var latestPayment in latestPayments)
                if (pendingBookings.Contains(latestPayment.Key))
                {
                    try
                    {
                        await OnPaymentNotified(latestPayment.Value);
                    }
                    catch(Exception ex)
                    {
                        //Record this
                    }
                }
        }

        private async Task<IEnumerable<KeyValuePair<string, string>>> LatestPayments(long offset, long limit)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            var latestPayments = await _mp.SearchPaymentAsync(new Dictionary<string, string>(), offset, limit);
            var paymentsResults = ((latestPayments["response"] as Hashtable)["results"] as ArrayList);
            foreach (Hashtable payment in paymentsResults)
            {
                string bookingId = (payment["collection"] as Hashtable)["external_reference"]?.ToString();
                string id = (payment["collection"] as Hashtable)["id"]?.ToString();
                result.Add(new KeyValuePair<string, string>(bookingId, id));
            }
            return result;
        }


        [Route("OnPaymentNotified/{notificationId}")]
        public async Task<IActionResult> OnPaymentNotified(string notificationId)
        {
            var payment = await _mp.GetPaymentAsync(notificationId);
            if (payment["status"]?.ToString() == "200")
            {
                PaymentNotice paymentInfo = new PaymentNotice(((payment["response"] as Hashtable)["collection"] as Hashtable), _config.GetSection("LocalZone").Value);
                var merchantOrder = await _mp.GetMerchantOrderAsync(paymentInfo.Merchant_Order_Id);

                if (merchantOrder["status"]?.ToString() == "200")
                    paymentInfo.User_Id = (merchantOrder["response"] as Hashtable)["additional_info"]?.ToString();

                if (!String.IsNullOrWhiteSpace(paymentInfo.BookingId))
                    await _paymentNoticeRepository.CreatePayment(paymentInfo);
            }

            return Ok();
        }
    }
}
