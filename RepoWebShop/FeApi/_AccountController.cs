using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
		public async Task<bool> IsMobileConfirmed() => (await _account.Current()).PhoneNumberConfirmed;

		[HttpGet]
		[Route("IsAdmin")]
		public async Task<bool> IsAdmin() => await _account.IsAdmin();

		[HttpPost]
		[Route("SocialLogin")]
		public async Task<_User> SocialLogin()
		{
			var userData = (new JsonSerializer()).Deserialize<_ProviderData[]>(new JsonTextReader(new StreamReader(Request.Body))).FirstOrDefault();
			await _account.EnsureSocialLoginAsync(userData);
			var signedIn = await _signInManager.ExternalLoginSignInAsync(userData.ProviderId , userData.Uid, isPersistent: true, bypassTwoFactor: true);
			return signedIn.Succeeded ? _mapper.Map<ApplicationUser, _User>(await _userManager.FindByEmailAsync(userData.Email)) : null;
		}

		[HttpPost]
		[Route("SignOut")]
		public async Task<IActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return Ok();
		}

		[HttpPost]
		[Route("EmailLogin")]
		public async Task<_User> EmailLogin()
		{
			var userData = (new JsonSerializer()).Deserialize<_EmailLogin>(new JsonTextReader(new StreamReader(Request.Body)));
			var appUser = await _userManager.FindByEmailAsync(userData.Email);
			var signedIn = await _signInManager.PasswordSignInAsync(appUser, userData.Password, true, false);
			return signedIn.Succeeded ? _mapper.Map<ApplicationUser, _User>(appUser) : null;
		}
	}
}
