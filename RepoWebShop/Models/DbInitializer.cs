using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepoWebShop.Models
{
    public static class DbInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            CreateUserWithRoleAsync(serviceProvider, "Administrator", "sebastian.scd@gmail.com", "Sebastián", "Cabeza");
            CreateUserWithRoleAsync(serviceProvider, "Administrator", "fiorelcd@gmail.com", "Fiorella", "Cabeza");
            CreateUserWithRoleAsync(serviceProvider, "Administrator", "marcelardec@gmail.com", "Marcela", "Declich");
            CreateUserWithRoleAsync(serviceProvider, "Administrator", "cabeza1961@gmail.com", "Claudio", "Cabeza");
            CreateUserWithRoleAsync(serviceProvider, "Administrator", "luciebenve@gmail.com", "Lucía", "Benvenuto");
        }

        private static void CreateUserWithRoleAsync(IServiceProvider serviceProvider, string role, string email, string name, string lastname)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                 roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();

            ApplicationUser user = userManager.FindByNameAsync(email).GetAwaiter().GetResult();
            if (user == null)
            { 
                user = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    LastName = lastname,
                    FirstName = name
                };
                userManager.CreateAsync(user).Wait();
            }

            if (!userManager.IsInRoleAsync(user, role).GetAwaiter().GetResult())
                userManager.AddToRoleAsync(user, role).Wait();
        }
    }
}
