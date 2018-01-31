using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Extensions;
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
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailRepository _emailRepository;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _sender;
        private ApplicationUser _currentUser
        {
            get
            {
                return _userManager.Users.FirstOrDefault(x => x.NormalizedUserName.ToLower() == HttpContext.User.Identity.Name.ToLower());
            }
        }
        public AccountDataController(SignInManager<ApplicationUser> signInManager, IEmailRepository emailRepository, UserManager<ApplicationUser> userManager, IAccountRepository accountRepository, ShoppingCart shoppingCart, IConfiguration config, IOrderRepository orderRepository)
        {
            _signInManager = signInManager;
            _emailRepository = emailRepository;
            _accountRepository = accountRepository;
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _accountSid = config.GetSection("TwilioAccoundSid").Value;
            _authToken = config.GetSection("TwilioAuthToken").Value;
            _sender = config.GetSection("TwilioSender").Value;
            _userManager = userManager;
                     
        }
        [Authorize]
        [Route("ValidateEmail")]
        [HttpPost]
        public IActionResult ValidateEmail()
        {
            try
            {
                _emailRepository.SendEmailActivationAsync(_currentUser, Request.HostUrl());
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [Authorize]
        [Route("SendValidationSms/{number}")]
        [HttpPost]
        public async Task<IActionResult> SendValidationSms(string number)
        {
            number = HttpUtility.HtmlDecode(number);
            var token = await _accountRepository.SendValidationCode(_currentUser, number);
            if (String.IsNullOrEmpty(token))
                return BadRequest();
            return Ok();
        }

        [HttpPost]
        [Route("VerifyNumber/{number}")]
        public async Task<IActionResult> VerifyNumber(string number)
        {
            if (number == _currentUser.ValidationPhoneToken)
            {
                _currentUser.PhoneNumberConfirmed = true;
                _currentUser.PhoneNumber = _currentUser.PhoneNumberDeclared;
                await _userManager.UpdateAsync(_currentUser);
                return Ok();
            }
            else
            {
                return BadRequest("Número de activación no coincide.");
            }
        }

        [Authorize]
        [Route("GetUserName")]
        public async Task<IActionResult> GetUserName()
        {
            var user = await _userManager.GetUser(_signInManager);
            return Ok(new { name = user.FirstName});
        }
    }
}
