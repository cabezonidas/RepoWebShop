using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace RepoWebShop.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _sender;

        public AccountRepository(AppDbContext appDbContext, IConfiguration config, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _appDbContext = appDbContext;
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountSid = config.GetSection("TwilioAccoundSid").Value;
            _authToken = config.GetSection("TwilioAuthToken").Value;
            _sender = config.GetSection("TwilioSender").Value;
        }



        public string SendValidationCode(ApplicationUser user, string phone)
        {
            var token = String.Concat(DateTime.Now.Ticks.ToString().ToArray().TakeLast(4));

            TwilioClient.Init(_accountSid, _authToken);
            try
            {
                var result = MessageResource.Create(
                    from: new PhoneNumber(_sender),
                    to: new PhoneNumber("+" + phone),
                    body: "Tu código de activación es " + token + ". De las Artes.");

                user.ValidationPhoneToken = token;
                user.PhoneNumberDeclared = phone;

                _userManager.UpdateAsync(user).Wait();

                return token;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
