using Microsoft.AspNetCore.Identity;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RepoWebShop.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<ApplicationUser> GetUser(this UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser>  _signInManager)
        {
            return await userManager.GetUserAsync(_signInManager.Context.User);
        }
        public static ApplicationUser GetUserByExternalLogin(this UserManager<ApplicationUser> userManager, ExternalLoginInfo _info)
        {
            var email = _info.Principal.GetClaimValue(ClaimTypes.Email);
            var nameId = _info.Principal.GetClaimValue(ClaimTypes.NameIdentifier);
            return userManager.Users.FirstOrDefault(x => x.Email.ToLower() == email.ToLower() || x.FacebookNameIdentifier == nameId || x.GoogleNameIdentifier == nameId);
        }
    }
}
