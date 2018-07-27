using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Extensions;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepoWebShop.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _appDbContext;
		private readonly IDistributedCache _serverCache;
		private readonly IEmailRepository _emailRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISmsRepository _smsRepository;
        private readonly IMapper _mapper;
   

        public AccountRepository(IDistributedCache serverCache, IEmailRepository emailRepository, IMapper mapper, AppDbContext appDbContext, ISmsRepository smsRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _emailRepository = emailRepository;
            _mapper = mapper;
            _smsRepository = smsRepository;
            _appDbContext = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
			_serverCache = serverCache;
        }

		public async Task SetCacheEmailActivation(_RegisterEmail registration)
		{
			var data = registration.ToJson();
			await _serverCache.SetStringAsync(registration.Email, data, new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = new TimeSpan(24, 0, 0, 0)
			});
			await _emailRepository.SendEmailCodeAsync(registration);
		}

		public async Task<_RegisterEmail> GetCacheEmailActivation(string email)
		{
			var body = await _serverCache.GetStringAsync(email);
			var deserializedRegisterMail = body.Parse<_RegisterEmail>();
			return deserializedRegisterMail;
		}

		public async Task<IdentityResult> CreateOrUpdateUserAsync(ExternalLoginInfo info, string email, string hostUrl)
        {
            var nameIdentifier = info.Principal.GetClaimValue(ClaimTypes.NameIdentifier);
            var user = _userManager.Users.FirstOrDefault(x => x.GoogleNameIdentifier == nameIdentifier || x.FacebookNameIdentifier == nameIdentifier || x.Email == info.Principal.GetClaimValue(ClaimTypes.Email));

            email = String.IsNullOrEmpty(email) ? info.Principal.GetClaimValue(ClaimTypes.Email) : email;
            
            if (user == null)
            {
                return await CreateFromExternalLoginInfoAsync(info, email, hostUrl);
            }
            else
            {
                user = _mapper.Map(info, user);
                return await _userManager.UpdateAsync(user);
            }
        }

		public async Task EnsureSocialLoginAsync(_ProviderData info)
		{
			var user = await _userManager.FindByEmailAsync(info.Email);

			if (user == null)
			{
				var newUser = _mapper.Map<_ProviderData, ApplicationUser>(info);
				var creation = await _userManager.CreateAsync(newUser);
			}
			else
			{
				var updateUser = _mapper.Map(info, user);
				var update = await _userManager.UpdateAsync(updateUser);
			}

			var result = await _userManager.FindByEmailAsync(info.Email);
			var logins = await _userManager.GetLoginsAsync(result);

			if (logins.Count(x => x.LoginProvider == info.ProviderId) == 0)
			{
				var login = await _userManager.AddLoginAsync(result, new UserLoginInfo(info.ProviderId, info.Uid, info.DisplayName));
			}
		}

		private async Task<IdentityResult> CreateFromExternalLoginInfoAsync(ExternalLoginInfo info, string email, string hostUrl)
        {
            var newUser = _mapper.Map<ExternalLoginInfo, ApplicationUser>(info);
            if (String.IsNullOrEmpty(newUser.Email))
                newUser.Email = email;
            if (String.IsNullOrEmpty(newUser.UserName))
                newUser.UserName = email;
            var result = await _userManager.CreateAsync(newUser);
            if (result.Succeeded && !newUser.EmailConfirmed)
                await _emailRepository.SendEmailActivationAsync(newUser);
            return result;
		}

		public async Task<IdentityResult> EnsureUserHasLoginAsync(ExternalLoginInfo info, string email)
        {
            var user = _userManager.GetUserByExternalLogin(info);
            if (user != null)
            {
                var logins = await _userManager.GetLoginsAsync(user);
                if (logins.Count(x => x.LoginProvider == info.LoginProvider) == 0)
                    return await _userManager.AddLoginAsync(user, info);
                else
                    return IdentityResult.Success;
            }
            else
            {
                IdentityError[] errors = { new IdentityError { Code = "UserOrEmailNotFound", Description = "Es necesario el email." } };
                return IdentityResult.Failed(errors);
            }
        }
        public async Task<bool> IsAdmin()
        {
            var user = await _userManager.GetUser(_signInManager);
            return user != null && await _userManager.IsInRoleAsync(user, "Administrator");
        }
        public async Task<ApplicationUser> Current()
        {
            return await _userManager.GetUser(_signInManager);
        }
        public async Task<string> SendValidationCode(ApplicationUser user, string phone)
        {
            var token = String.Concat(DateTime.Now.Ticks.ToString().ToArray().TakeLast(4));
            
            try
            {
                await _smsRepository.SendSms(phone, $"Tu código de activación es R-{token}. De las Artes.");

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

		public async Task<_User> RegisterUser(_RegisterEmail userCache)
		{
			var appUser = _mapper.Map<_RegisterEmail, ApplicationUser>(userCache);
			var userCreated = await _userManager.CreateAsync(appUser, userCache.Password);
			appUser = await _userManager.FindByEmailAsync(appUser.Email);
			var signInUser = await _signInManager.PasswordSignInAsync(appUser, userCache.Password, true, false);
			return _mapper.Map<ApplicationUser, _User>(appUser);
		}

		public async Task UpdatePassword(string password)
		{
			var appUser = await Current(); 
			if (await _userManager.HasPasswordAsync(appUser))
				await _userManager.RemovePasswordAsync(appUser);
			await _userManager.AddPasswordAsync(appUser, password);
		}

		public async Task SetCacheMobileActivation(string mobile)
		{
			var appUser = await Current();
			appUser.PhoneNumberDeclared = mobile;
			await _userManager.UpdateAsync(appUser);

			var _mobileInfo = new _MobileInfo { Number = mobile, Code = (new Random()).Next(1000, 9999).ToString() };

			await _serverCache.SetStringAsync($"{appUser.Email}/mobile", _mobileInfo.ToJson(), new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = new TimeSpan(24, 0, 0, 0)
			});

			await _smsRepository.SendSms(mobile, $"Código {_mobileInfo.Code}. Utiliza este número para confirmar tu celular. De las Artes.");
		}

		public async Task<_MobileInfo> GetCacheMobileActivation()
		{
			var appUser = await Current();
			var body = await _serverCache.GetStringAsync($"{appUser.Email}/mobile");
			var mobileInfo = body.Parse<_MobileInfo>();
			return mobileInfo;
		}

		public async Task UpdateMobileAsync(string number)
		{
			var appUser = await Current();
			appUser.PhoneNumber = number;
			appUser.PhoneNumberConfirmed = true;
			await _userManager.UpdateAsync(appUser);
		}
	}
}
