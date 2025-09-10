using Company.Project.Domain.Models;
using Company.Project.MVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Company.Project.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;

        public AccountController(UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager)
        {
            UserManager = _userManager;
            SignInManager = _signInManager;
        }

        public IActionResult Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel NewAccount)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = NewAccount.UserName,
                    Email = NewAccount.Email,
                    FirstName = NewAccount.FirstName,
                    LastName = NewAccount.LastName,
                    NID = NewAccount.NID
                };

                IdentityResult result = await UserManager.CreateAsync(user, NewAccount.Password);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(NewAccount);
        }

        public IActionResult Login(string ReturnUrl = "~/Home/Index")
        {
            if (!User.Identity.IsAuthenticated)
            {
                ViewData["Redirect Url"] = ReturnUrl;
                return View();
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel LoginUser, string ReturnUrl = "~Home/Index")
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(LoginUser.UserName);
                if (user != null)
                {
                    var result = await SignInManager.PasswordSignInAsync(user,LoginUser.Password,LoginUser.RemmemberMe,false);
                    if (result.Succeeded)
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("","Invalid UserName Or Password");
                    }
                }
                else
                {
                    ModelState.AddModelError("","Invalid UserName Or Password");
                }
            }
            return View(LoginUser);
        }

        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
