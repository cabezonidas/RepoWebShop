using Microsoft.AspNetCore.Hosting;
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
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            await CreateUserWithRoleAsync(serviceProvider, "Administrator", "sebastian.scd@gmail.com", "Sebastián", "Cabeza");
            await CreateUserWithRoleAsync(serviceProvider, "Administrator", "fiorelcd@gmail.com", "Fiorella", "Cabeza");
            await CreateUserWithRoleAsync(serviceProvider, "Administrator", "marcelardec@gmail.com", "Marcela", "Declich");
            await CreateUserWithRoleAsync(serviceProvider, "Administrator", "cabeza1961@gmail.com", "Claudio", "Cabeza");
            await CreateUserWithRoleAsync(serviceProvider, "Administrator", "luciebenve@gmail.com", "Lucía", "Benvenuto");

            var hostEnvironment = serviceProvider.GetRequiredService<IHostingEnvironment>();
            if (!hostEnvironment.IsProduction())
            {
                await CreateUserWithRoleAsync(serviceProvider, "Administrator", "santiag0.l0pez@hotmail.com", "Santiago", "Lopez");
                await CreateUserWithRoleAsync(serviceProvider, "Administrator", "j_dalmasso@outlook.com", "Juan", "Dalmasso");
            }
        }

        private static async Task CreateUserWithRoleAsync(IServiceProvider serviceProvider, string role, string email, string name, string lastname)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!await roleManager.RoleExistsAsync(role))
                 await roleManager.CreateAsync(new IdentityRole(role));

            ApplicationUser user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = email,
                    LastName = lastname,
                    Email = email,
                    EmailConfirmed = true,
                    FirstName = name
                };
                await userManager.CreateAsync(user);
            }

            if (!await userManager.IsInRoleAsync(user, role))
                await userManager.AddToRoleAsync(user, role);
        }
    }
}
