using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RepoWebShop.Extensions;
using RepoWebShop.FeModels;
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
        private readonly IEmailRepository _emailRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ISmsRepository _smsRepository;
        private readonly IMapper _mapper;
   

        public AccountRepository(IEmailRepository emailRepository, IMapper mapper, AppDbContext appDbContext, ISmsRepository smsRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _emailRepository = emailRepository;
            _mapper = mapper;
            _smsRepository = smsRepository;
            _appDbContext = appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
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
			var user = _userManager.Users
				.FirstOrDefault(x => x.GoogleNameIdentifier == info.Uid || x.FacebookNameIdentifier == info.Uid || x.Email == info.Email);

			var appUser = _mapper.Map(info, user ?? new ApplicationUser());

			if (user == null)
				await _userManager.CreateAsync(appUser);
			else
				await _userManager.UpdateAsync(appUser);

			var result = await _userManager.FindByEmailAsync(appUser.Email);
			var logins = await _userManager.GetLoginsAsync(result);

			if (logins.Count(x => x.LoginProvider.ToLower() == info.ProviderId.ToLower()) == 0)
				await _userManager.AddLoginAsync(user, new UserLoginInfo(info.ProviderId, info.Uid, info.DisplayName));
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
    }
}
