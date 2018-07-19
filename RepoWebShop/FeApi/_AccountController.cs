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
		[Route("Current")]
		public async Task<_User> Current()
		{
			return _mapper.Map<ApplicationUser, _User>(await _account.Current());
		}

		[HttpGet]
		[Route("IsAdmin")]
		public async Task<bool> IsAdmin()
		{
			return await _account.IsAdmin();
		}

		[HttpPost]
		[Route("ExternalSignIn")]
		public async Task<_User> ExternalSignIn()
		{
			var info = await _signInManager.GetExternalLoginInfoAsync();

			await _account.CreateOrUpdateUserAsync(info, null, Request.HostUrl());
			await _account.EnsureUserHasLoginAsync(info, null);

			await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);

			return _mapper.Map<ApplicationUser, _User>(await _account.Current());
		}
	}
}
