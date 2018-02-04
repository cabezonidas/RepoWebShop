using Microsoft.AspNetCore.Identity;
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
        Task<IdentityResult> CreateOrUpdateUserAsync(ExternalLoginInfo info, string email, string hostUrl);
        Task<IdentityResult> EnsureUserHasLoginAsync(ExternalLoginInfo info, string email);
    }
}
