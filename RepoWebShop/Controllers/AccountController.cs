using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RepoWebShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using RepoWebShop.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            return View(new LoginViewModel
            {
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
                ReturnUrl = returnUrl
            });
        }

        [AllowAnonymous]
        public IActionResult ExternalLogin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            //var user = _mapper.Map<IdentityUser, ApplicationUser>(await _userManager.FindByNameAsync(loginViewModel.UserName));
            var user = await _userManager.FindByNameAsync(loginViewModel.UserName);

            if (user != null)
            {
                try
                {
                    //var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, user.Password, true, lockoutOnFailure: false);
                    user.Email = user.NormalizedEmail;
                    user.UserName = loginViewModel.UserName;
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);

                    if (result.Succeeded)
                    {
                        if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                            return RedirectToAction("Index", "Home");

                        return Redirect(loginViewModel.ReturnUrl);
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(loginViewModel);
                }
            }

            ModelState.AddModelError("", "Usuario/contraseña no encontrada");
            return View(loginViewModel);
        }


        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(ApplicationUser registration)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(registration, registration.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(registration);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
