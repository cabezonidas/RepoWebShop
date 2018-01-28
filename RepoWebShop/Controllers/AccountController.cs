using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RepoWebShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using RepoWebShop.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using RepoWebShop.Interfaces;
using System.Security.Cryptography;
using RepoWebShop.Extensions;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RepoWebShop.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAccountRepository _accountRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly IMapper _mapper;
        private ApplicationUser _currentUser
        {
            get
            {
                return _userManager.Users.FirstOrDefault(x => x.NormalizedUserName.ToLower() == HttpContext.User.Identity.Name.ToLower());
            }
        }
            

        public AccountController(IEmailRepository emailRepository, IAccountRepository accountRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _emailRepository = emailRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _accountRepository = accountRepository;
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
        [Route("[Controller]/EmailVerification/{user}/{hash}")]
        public IActionResult EmailVerification(string user, string hash)
        {
            ApplicationUser appUser = _userManager.Users.FirstOrDefault(x => x.UserName.ToLower() == user.ToLower());
            if(appUser == null)
            {
                return NotFound();
            }

            string savedHash = SHA256.Create().FromString(appUser.ValidationMailToken.ToString());

            if (savedHash == hash)
            {
                appUser.EmailConfirmed = true;
                _userManager.UpdateAsync(appUser);
            }
            var result = _mapper.Map<ApplicationUser, EmailValidationViewModel>(appUser);

            return View(result);
        }
        
        [AllowAnonymous]
        [Route("[Controller]/EmailVerificationBodyEmail/{user}/{hash}")]
        public IActionResult EmailVerificationBodyEmail(string user, string hash)
        {
            ApplicationUser appUser = _userManager.Users.FirstOrDefault(x => x.UserName.ToLower() == user.ToLower());
            var result = _mapper.Map<ApplicationUser, EmailValidationViewModel>(appUser);
            result.HostUrl = Request.HostUrl();
            result.Hash = hash;
            return View(result);
        }

        [AllowAnonymous]
        public IActionResult ExternalLogin()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult PrivacyPolicy()
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
                catch (Exception ex)
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
        public async Task<IActionResult> Register(RegisterViewModel registration)
        {
            var user = _mapper.Map<RegisterViewModel, ApplicationUser>(registration);
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(user, registration.Password);

                if (result.Succeeded)
                {
                    _emailRepository.SendEmailActivationAsync(user, Request.HostUrl());
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(registration);
        }

        [Authorize]
        public IActionResult ChangePassword()
        {
            return View(new PasswordChangeViewModel());
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordChangeViewModel passwordChange)
        {
            if (ModelState.IsValid)
            {
                if(passwordChange.New != passwordChange.NewRepeated)
                {
                    passwordChange.ErrorMsg = "La contraseña nueva no coincide";
                    return View(passwordChange);
                }

                var result = await _userManager.ChangePasswordAsync(_currentUser, passwordChange.Current, passwordChange.New);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    passwordChange.ErrorMsg = result.Errors.FirstOrDefault()?.Description ?? string.Empty;
                }
            }
            return View(passwordChange);
        }

        [Authorize]
        public IActionResult Profile()
        {
            
            return View(_currentUser);
        }

        [Authorize]
        public IActionResult ProfileDetails()
        {
            var result = _mapper.Map<ApplicationUser, ApplicationUserViewModel>(_currentUser);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileDetails(ApplicationUserViewModel appUserViewModel)
        {
            var currentUser = _currentUser;

            currentUser = _mapper.Map(appUserViewModel, currentUser);

            if (ModelState.IsValid)
            {
                var result = await _userManager.UpdateAsync(currentUser);

                if (result.Succeeded)
                {
                    return RedirectToAction("Profile", "Account");
                }
                else
                {
                    var error = result.Errors.FirstOrDefault();
                    if(error != null)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    appUserViewModel.ErrorMsg = result.Errors.FirstOrDefault()?.Description ?? string.Empty;
                }
            }
            return View(appUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("[Controller]/VerifyNumber/{controllerName}/{actionName}/")]
        public IActionResult VerifyNumber(string controllerName, string actionName)
        {
            var result = new AppUserValidateViewModel()
            {
                Controller = controllerName,
                Action = actionName
            };
            return View(result);
        }

        [HttpGet]
        public IActionResult VerifyNumber()
        {
            return View(new AppUserValidateViewModel());
        }
    }
}
