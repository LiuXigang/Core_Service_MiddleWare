using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_Service_MiddleWare.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core_Service_MiddleWare.Controllers
{
    public class AccoutController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccoutController(
            SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var user =await _userManager.FindByNameAsync(viewModel.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, viewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index2","Home");
                }
            }
            ModelState.AddModelError("", "用户名/密码错误！");
            return View(viewModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = viewModel.UserName.Trim()
                };
                var result = await _userManager.CreateAsync(user, viewModel.Password.Trim());
                if (result.Succeeded)
                {
                    return RedirectToAction("Index2", "Home");
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Json(true);
        }
    }
}