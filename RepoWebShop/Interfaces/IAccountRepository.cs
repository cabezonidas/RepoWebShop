using Microsoft.AspNetCore.Identity;
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
		Task SetCacheRegistration(_RegisterEmail registration);
		Task<_RegisterEmail> GetCacheRegistration(string email);
		Task<_User> RegisterUser(_RegisterEmail userCache);
	}
}
