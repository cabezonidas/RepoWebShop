using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using RepoWebShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using RepoWebShop.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using RepoWebShop.Interfaces;
using System.Security.Cryptography;
using RepoWebShop.Extensions;
using System.Security.Claims;

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
        //private readonly ILogger _logger;


        public AccountController(IEmailRepository emailRepository, IAccountRepository accountRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            //_logger = logger;
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

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterProviderWithMail(ApplicationUser appuser)
        {
            var register = _mapper.Map<ApplicationUser, RegisterProviderWithMailViewModel>(appuser);

            return View(register);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterProviderWithMail(RegisterProviderWithMailViewModel infoVm)
        {
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
                return RedirectToAction("Login");

            var userExists = await _accountRepository.CreateOrUpdateUserAsync(info, infoVm.Email, Request.HostUrl());
            if(!userExists.Succeeded)
            {
                var user = _mapper.Map<RegisterProviderWithMailViewModel, ApplicationUser>(infoVm);
                infoVm.Errors = new List<string>();
                infoVm.Errors.AddRange(userExists.Errors.Select(x => x.Description));
                return View(infoVm);
            }

            var hasLogin = await _accountRepository.EnsureUserHasLoginAsync(info, infoVm.Email);
            if (hasLogin.Succeeded)
            {
                var loggedIn = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
                if (loggedIn.Succeeded)
                    return RedirectToAction("List", "PieDetail");
            }
            return RedirectToAction("Register", "Account");
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
                    await _signInManager.PasswordSignInAsync(user, registration.Password, false, false);
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
                IdentityResult result;
                var currentUser = await _userManager.GetUser(_signInManager);
                if (!await _userManager.HasPasswordAsync(currentUser))
                    result = await _userManager.AddPasswordAsync(currentUser, passwordChange.New);
                else
                    result = await _userManager.ChangePasswordAsync(currentUser, passwordChange.Current, passwordChange.New);
                
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
        public async Task<IActionResult> Profile()
        {
            
            return View(await _userManager.GetUser(_signInManager));
        }

        [Authorize]
        public async Task<IActionResult> ProfileDetails()
        {
            var currentUser = await _userManager.GetUser(_signInManager);

            var result = _mapper.Map<ApplicationUser, ApplicationUserViewModel>(currentUser);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> ProfileDetails(ApplicationUserViewModel appUserViewModel)
        {
            var currentUser = await _userManager.GetUser(_signInManager);

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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Action(nameof(ExternalLoginCallback), "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            var result = Challenge(properties, provider);
            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            var loginViewModel = new LoginViewModel { Errors = new List<string>(), ReturnUrl = returnUrl };
            if (remoteError != null)
            {
                loginViewModel.Errors.Add(remoteError);
                return RedirectToAction(nameof(Login), loginViewModel);
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                loginViewModel.Errors.Add("No pudimos recibir los datos de autenticación. Por favor, intentá de nuevo.");
                return RedirectToAction(nameof(Login), loginViewModel);
            }

            var userExists = await _accountRepository.CreateOrUpdateUserAsync(info, null, Request.HostUrl());
            if(!userExists.Succeeded)
            {
                var missingEmailError = userExists.Errors.Select(x => x.Code).Count(y => y == "InvalidEmail" || y == "InvalidUserName") == userExists.Errors.Count();

                if (missingEmailError)
                {
                    var appuser = _mapper.Map<ExternalLoginInfo, ApplicationUser>(info);
                    return RedirectToAction(nameof(AccountController.RegisterProviderWithMail), appuser);
                }
                else
                {
                    loginViewModel.Errors.AddRange(userExists.Errors.Select(x => x.Description));
                    return RedirectToAction(nameof(Login), loginViewModel);
                }
            }

            var result = await _accountRepository.EnsureUserHasLoginAsync(info, null);
            if (result.Succeeded)
            {
                var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
                if (signInResult.Succeeded)
                {
                    if(!string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                        return Redirect(loginViewModel.ReturnUrl);
                    else
                        return RedirectToAction(nameof(PieDetailController.List), "PieDetail");
                }
                else
                {
                    loginViewModel.Errors.Add("Tu usario no pudo iniciar sesión. Por favor, prueba otro medio.");
                    return RedirectToAction(nameof(Login), loginViewModel);
                }
            }
            else
            {
                loginViewModel.Errors.Add("No pudimos crear el inicio de sesión. Por favor, intentá otro medio.");
                return RedirectToAction(nameof(Login), loginViewModel);
            }
        }
    }
}
