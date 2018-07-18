using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.Extensions;
using RepoWebShop.FeModels;
using RepoWebShop.Interfaces;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.FeApi
{
	[Route("api/[controller]")]
	public class _AccountController : Controller
	{
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly IAccountRepository _account;
		private readonly IMapper _mapper;

		public _AccountController(IAccountRepository account, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
		{
			_mapper = mapper;
			_signInManager = signInManager;
			_account = account;
		}

		[HttpGet]
		[Route("ExternalLoginProviders")]
		public async Task<IEnumerable<string>> ExternalLoginProviders()
		{
			return (await _signInManager.GetExternalAuthenticationSchemesAsync()).Select(x => x.Name);
		}


		[HttpPost]
		[Route("ExternalLogin/{provider}")]
		public IActionResult ExternalLogin(string provider, [FromBody] string returnUrl = null)
		{
			var redirectUrl = Url.Action(nameof(ExternalLoginCallback), RouteData.Values["controller"].ToString(), new { returnUrl });
			var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
			var result = Challenge(properties, provider);
			
			return result;
		}

		[HttpGet]
		[Route("ExternalLoginCallback")]
		public async Task<_User> ExternalLoginCallback([FromQuery] string returnUrl = null, [FromQuery] string remoteError = null)
		{
			IEnumerable<string> errors = new string[0];

			if (remoteError != null)
				throw new Exception(remoteError);

			var info = await _signInManager.GetExternalLoginInfoAsync();

			await _account.CreateOrUpdateUserAsync(info, null, Request.HostUrl());
			await _account.EnsureUserHasLoginAsync(info, null);

			await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);

			return _mapper.Map<ApplicationUser, _User>(await _account.Current());
		}
	}
}
