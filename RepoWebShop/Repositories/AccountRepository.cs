using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly IMapper _mapper;
        private readonly string _accountSid;
        private readonly string _authToken;
        private readonly string _sender;        

        public AccountRepository(IMapper mapper, AppDbContext appDbContext, IConfiguration config, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _mapper = mapper;
            _appDbContext = appDbContext;
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountSid = config.GetSection("TwilioAccoundSid").Value;
            _authToken = config.GetSection("TwilioAuthToken").Value;
            _sender = config.GetSection("TwilioSender").Value;
        }

        public async Task<IdentityResult> CreateOrUpdateUserAsync(ExternalLoginInfo info)
        {
            var nameIdentifier = info.Principal.GetClaimValue(ClaimTypes.NameIdentifier);
            var email = info.Principal.GetClaimValue(ClaimTypes.Email);
            var user = _userManager.Users.FirstOrDefault(x => x.NameIdentifier == nameIdentifier || x.Email == email);

            if (user == null)
            {
                user = _mapper.Map<ExternalLoginInfo, ApplicationUser>(info);
                return await _userManager.CreateAsync(user);
            }
            else
            {
                user = _mapper.Map(info, user);
                return await _userManager.UpdateAsync(user);
            }
        }

        public async Task<IdentityResult> EnsureUserHasLoginAsync(ExternalLoginInfo info)
        {
            var userExists = await CreateOrUpdateUserAsync(info);
            if (userExists.Succeeded)
            {
                var user = _userManager.GetUserByExternalLogin(info);
                var logins = await _userManager.GetLoginsAsync(user);
                if (logins.Count(x => x.LoginProvider == info.LoginProvider) == 0)
                    return await _userManager.AddLoginAsync(user, info);
                else
                    return IdentityResult.Success;
            }
            return userExists;
        }

        public async Task<string> SendValidationCode(ApplicationUser user, string phone)
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

                await _userManager.UpdateAsync(user);

                return token;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
