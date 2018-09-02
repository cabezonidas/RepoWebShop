using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepoWebShop.Extensions;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _AccountController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IAccountRepository _account;
		private readonly IMapper _mapper;

		public _AccountController(IAccountRepository account, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
			_signInManager = signInManager;
			_account = account;
		}

		[HttpGet]
		[Route("Current")]
		public async Task<_User> Current() => _mapper.Map<ApplicationUser, _User>(await _account.Current());

		[HttpGet]
		[Route("IsAuth")]
		public async Task<bool> IsAuth() => (await _account.Current()) != null;

		[HttpGet]
		[Route("IsMobileConfirmed")]
		public async Task<bool> IsMobileConfirmed() => (await _account.Current())?.PhoneNumberConfirmed ?? false;

		[HttpGet]
		[Route("IsAdmin")]
		public async Task<bool> IsAdmin() => await _account.IsAdmin();

		[HttpGet]
		[Route("IsEmailAvailable/{email}")]
		public async Task<bool> IsEmailAvailable(string email) => await _userManager.FindByEmailAsync(email) == null;

		[HttpPost]
		[Route("SignOut")]
		// Not working
		public async Task SignOut()
		{
			await HttpContext.SignOutAsync();
			await _signInManager.SignOutAsync();
		}

		[HttpPost]
		[Route("SocialLogin")]
		public async Task<_User> SocialLogin()
		{
			var userData = Request.ParseBody<_ProviderData[]>().FirstOrDefault();
			await _account.EnsureSocialLoginAsync(userData);
			var signInResult = await _signInManager.ExternalLoginSignInAsync(userData.ProviderId, userData.Uid, isPersistent: true, bypassTwoFactor: true);
			return signInResult.Succeeded ? _mapper.Map<ApplicationUser, _User>(await _userManager.FindByEmailAsync(userData.Email)) : null;
		}

		[HttpPost]
		[Route("EmailLogin")]
		public async Task<_User> EmailLogin()
		{
			var userData = Request.ParseBody<_EmailLogin>();
			var appUser = await _userManager.FindByEmailAsync(userData.Email);
			if (appUser == null)
				return null;
			var signedIn = await _signInManager.PasswordSignInAsync(appUser, userData.Password, true, false);
			if (!signedIn.Succeeded)
				return null;
			return _mapper.Map<ApplicationUser, _User>(appUser);
		}

		[HttpPost]
		[Route("BookEmail")]
		public async Task<IActionResult> BookEmail()
		{
			var registration = Request.ParseBody<_RegisterEmail>();
			registration.ValidationCode = (new Random()).Next(1000, 9999).ToString();
			await _account.SetCacheEmailActivation(registration);
			return Ok();
		}

		[HttpPost]
		[Route("RegisterEmail/{emailCode}")]
		public async Task<_User> RegisterEmail(string emailCode)
		{
			var registration = Request.ParseBody<_RegisterEmail>(); 
			var userCache = await _account.GetCacheEmailActivation(registration.Email);
			return userCache.ValidationCode == emailCode ? await _account.RegisterUser(userCache) : null;
		}

		[HttpPost]
		[Route("RecoverEmail/{email}")]
		public async Task<IActionResult> RecoverEmail(string email)
		{
			ApplicationUser appUser = await _userManager.FindByEmailAsync(email);
			var regEmail = _mapper.Map<ApplicationUser, _RegisterEmail>(appUser);
			regEmail.ValidationCode = (new Random()).Next(1000, 9999).ToString();
			await _account.SetCacheEmailActivation(regEmail);
			return Ok();
		}

		[HttpPost]
		[Route("ActivateRecoveredEmail/{email}/{code}")]
		public async Task<_User> ActivateRecoveredEmail(string email, string code)
		{
			var userCache = await _account.GetCacheEmailActivation(email);
			if (userCache.ValidationCode != code)
				return null;
			else
			{
				var appUser = await _userManager.FindByEmailAsync(email);
				await _signInManager.SignInAsync(appUser, true);
				return _mapper.Map<ApplicationUser, _User>(appUser);
			}
		}

		[HttpPost]
		[Authorize]
		[Route("UpdatePassword/{password}")]
		public async Task UpdatePassword(string password) => await _account.UpdatePassword(password);

		[HttpPost]
		[Authorize]
		[Route("RegisterMobile/{mobile}")]
		public async Task RegisterMobile(string mobile) => await _account.SetCacheMobileActivation(mobile);

		[HttpPost]
		[Authorize]
		[Route("ConfirmMobile/{code}")]
		public async Task<bool> ConfirmMobile(string code)
		{
			_MobileInfo mobileInfo = await _account.GetCacheMobileActivation();
			if (mobileInfo.Code == code)
			{
				await _account.UpdateMobileAsync(mobileInfo.Number);
				return true;
			}
			else
				return false;
		}
	}
}
