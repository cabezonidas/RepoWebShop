using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RepoWebShop.FeModels;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Interfaces
{
    public interface IAccountRepository
    {
        Task<string> SendValidationCode(ApplicationUser user, string phone);
        Task<bool> IsAdmin();
        Task<IdentityResult> CreateOrUpdateUserAsync(ExternalLoginInfo info, string email, string hostUrl);
        Task<IdentityResult> EnsureUserHasLoginAsync(ExternalLoginInfo info, string email);
		Task<ApplicationUser> Current();
		Task EnsureSocialLoginAsync(_ProviderData userData);
		Task SetCacheEmailActivation(_RegisterEmail registration);
		Task<_RegisterEmail> GetCacheEmailActivation(string email);
		Task<_User> RegisterUser(_RegisterEmail userCache);
		Task UpdatePassword(string password);
		Task SetCacheMobileActivation(string mobile);
		Task<_MobileInfo> GetCacheMobileActivation(string email = null);
		Task UpdateMobileAsync(string number);
		Task<IEnumerable<_Customer>> GetCustomers();
		Task<string> GetEmailActivationCode(string email);
		Task<string> GetMobileActivationCode(string userId);
		Task ActivateEmail(string userId);
		Task ActivateMobile(string userId);
	}
}
