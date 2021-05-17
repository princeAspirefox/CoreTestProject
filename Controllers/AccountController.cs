using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityInCore.Models;
using IdentityInCore.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityInCore.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserContext context;

        /// <summary>
        /// fn to intializing field userManager and signInManager ,context
        /// </summary>
        /// <param name="userManager"> type UserManager<IdentityUser></param>
        /// <param name="signInManager">type SignInManager<IdentityUser></param>
        /// <param name="context"> UserContext</param>
        public AccountController(UserManager<IdentityUser> userManager,
                                      SignInManager<IdentityUser> signInManager,UserContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.context = context;
        }

        /// <summary>
        /// fn to return new user registration view
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }


        /// <summary>
        /// fn to creat new user 
        /// </summary>
        /// <param name="model">argument of type RegisterViewModel  </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }

        /// <summary>
        /// fn to return User login view
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// fn to Authenticate user
        /// </summary>
        /// <param name="user">Identity user </param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login( LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }

        /// <summary>
        /// fn to sign out User
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

       
    }
}
