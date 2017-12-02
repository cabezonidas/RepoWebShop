using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace RepoWebShop.Controllers
{
    [Route("api/[controller]")]
    public class AccountDataController : Controller
    {
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderRepository _orderRepository;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _sender;

        public AccountDataController(ShoppingCart shoppingCart, IConfiguration config, IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _accountSid = config.GetSection("TwilioAccoundSid").Value;
            _authToken = config.GetSection("TwilioAuthToken").Value;
            _sender = config.GetSection("TwilioSender").Value;
        }

        [Authorize]
        [Route("SendText/{number}")]
        [HttpPost]
        public IActionResult SendText(string number)
        {
            number = HttpUtility.HtmlDecode(number);
            var ticks = DateTime.Now.Ticks.ToString();
            var token = ticks.Substring(ticks.Length - 4, 4);

            _shoppingCart.AddValidationNumber(token);

            TwilioClient.Init(_accountSid, _authToken);

            try
            {
                var result = MessageResource.Create(
                    from: new PhoneNumber(_sender),
                    to: new PhoneNumber("+" + number),
                    body: "Tu código es " + token + ". Gracias por tu reserva. La reposteria.");
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        
        [Authorize]
        [Route("VerifyCode/{number}")]
        [HttpGet]
        public IActionResult VerifyCode(string number)
        {
            string generatedCode = _shoppingCart.GetValidationNumber();
            if(number == generatedCode)
            {
                _shoppingCart.ValidatePhone(number);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
