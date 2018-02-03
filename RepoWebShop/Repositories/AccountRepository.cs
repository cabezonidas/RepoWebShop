using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Extensions;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RepoWebShop.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISmsRepository _smsRepository;
        private readonly IMapper _mapper;
   

        public AccountRepository(IMapper mapper, AppDbContext appDbContext, ISmsRepository smsRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _mapper = mapper;
            _smsRepository = smsRepository;
            _appDbContext = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
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
            
            try
            {
                _smsRepository.SendSms($"+{phone}", $"Tu código de activación es {token}. De las Artes.");

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
