using Microsoft.AspNetCore.Identity;
using RepoWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Extensions
{
    public static class UserManagerExtension
    {
        public static async Task<ApplicationUser> GetUser(this UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser>  _signInManager)
        {
            return await userManager.GetUserAsync(_signInManager.Context.User);
        }
    }
}
