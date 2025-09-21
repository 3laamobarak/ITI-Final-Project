using System.Security.Claims;
using Company.Project.Domain.Models;
using Company.Project.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.Project.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> UserManager;
        private readonly SignInManager<ApplicationUser> SignInManager;
        private readonly RoleManager<IdentityRole> RoleManager;

        public AccountController(UserManager<ApplicationUser> _userManager,
            SignInManager<ApplicationUser> _signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            UserManager = _userManager;
            SignInManager = _signInManager;
            RoleManager = roleManager;
        }
        [Authorize("admin")]
        public async Task <IActionResult> GetAll()
        {
            var users =UserManager.Users.Select(u=>new UserViewModel
            {
                Id=u.Id,
                UserName=u.UserName,
                Email=u.Email,
                FirstName=u.FirstName,
                LastName=u.LastName,
                Nid=u.NID
            }).ToList();
            var model = new GetAllUsersViewModel
            {
                Users = users
            };
            return View(users);
        }
        [Authorize("admin")]
        public async Task <IActionResult> GetById(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [Authorize("admin")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var result = await UserManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("GetAll");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("Details", user);
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
        public async Task<IActionResult> Register(RegisterViewModel newAccount)
        {
            if (ModelState.IsValid)
            {
                // Check for duplicate username
                if (await UserManager.FindByNameAsync(newAccount.UserName) != null)
                {
                    ModelState.AddModelError("UserName", "Username is already taken.");
                    return View(newAccount);
                }

                // Check for duplicate email
                if (await UserManager.FindByEmailAsync(newAccount.Email) != null)
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                    return View(newAccount);
                }

                var user = new ApplicationUser
                {
                    UserName = newAccount.UserName,
                    Email = newAccount.Email,
                    FirstName = newAccount.FirstName,
                    LastName = newAccount.LastName,
                    NID = newAccount.NID
                };

                var result = await UserManager.CreateAsync(user, newAccount.Password);
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
            return View(newAccount);
        }
        public IActionResult Login(string ReturnUrl = "/Home/index")
        {
            if (!User.Identity.IsAuthenticated)
            {
                ViewData["Redirect Url"] = ReturnUrl;
                return View();
            }
            return RedirectToAction("Error", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginUser, string returnUrl = "/Home/Index")
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(loginUser.UserName);
                if (user != null)
                {
                    var result = await SignInManager.PasswordSignInAsync(user, loginUser.Password, loginUser.RemmemberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        // Add claims if needed
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim("FullName", $"{user.FirstName} {user.LastName}")
                        };

                        await SignInManager.SignInWithClaimsAsync(user, loginUser.RemmemberMe, claims);
                        return LocalRedirect(returnUrl);
                    }
                    else if (result.IsLockedOut)
                    {
                        ModelState.AddModelError("", "This account is locked out.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(loginUser);
        }
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [Authorize("admin")]
        public IActionResult AddRole()
        {
            ViewBag.Roles = RoleManager.Roles.ToList();
            return View();
        }
        [Authorize("admin")]
        [HttpPost]    
        public async Task<IActionResult> SaveRole(RoleViewModel roleViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = roleViewModel.RoleName;
                IdentityResult result = await RoleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    ViewBag.Message = true;
                    return RedirectToAction("AddRole");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("AddRole", roleViewModel);
        }
        [Authorize("admin")]
        public IActionResult AssignRole()
        {
            var users = UserManager.Users.ToList();
            var userRoles = users
                .Select(user => new
                {
                    UserName = user.UserName,
                    Roles = UserManager.GetRolesAsync(user).Result 
                })
                .Where(ur => ur.Roles.Any())
                .ToList();

            ViewBag.Users = users.Select(u => new SelectListItem
            {
                Value = u.UserName,
                Text = $"{u.FirstName} {u.LastName} ({u.UserName})"
            }).ToList();

            ViewBag.Roles = RoleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            ViewBag.UserRoles = userRoles;

            return View();
        }
        [HttpPost]
        [Authorize("admin")]
        public async Task<IActionResult> AssignRole(AssignRoleViewModel assignRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(assignRole.UserName);
                if (user != null)
                {
                    var role = await RoleManager.FindByNameAsync(assignRole.RoleName);
                    if (role != null)
                    {
                        if (await UserManager.IsInRoleAsync(user, assignRole.RoleName))
                        {
                            ModelState.AddModelError(string.Empty, "User already has this role.");
                        }
                        else
                        {
                            var result = await UserManager.AddToRoleAsync(user, assignRole.RoleName);
                            if (result.Succeeded)
                            {
                                ViewBag.Message = true;
                                return RedirectToAction("AssignRole");
                            }
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Role Name");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid User Name");
                }
            }

            return AssignRole(); // Re-populate ViewBag and return the view
        }
        [Authorize("admin")]
        public IActionResult RemoveRole(string username)
        {
            var user = UserManager.Users.FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return NotFound();
            }
            var roles = UserManager.GetRolesAsync(user).Result;
            ViewBag.Users = new List<SelectListItem>
            {
                new SelectListItem { Value = user.UserName, Text = user.UserName }
            };

            ViewBag.Roles = roles.Select(r => new SelectListItem
            {
                Value = r,
                Text = r
            }).ToList();

            return View(new AssignRoleViewModel { UserName = user.UserName });
        }
        [Authorize("admin")]
        [HttpPost]
        public async Task<IActionResult> RemoveRole(AssignRoleViewModel assignRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(assignRole.UserName);
                if (user != null)
                {
                    var role = await RoleManager.FindByNameAsync(assignRole.RoleName);
                    if (role != null)
                    {
                        if (await UserManager.IsInRoleAsync(user, assignRole.RoleName))
                        {
                            var result = await UserManager.RemoveFromRoleAsync(user, assignRole.RoleName);
                            if (result.Succeeded)
                            {
                                ViewBag.Message = "Role removed successfully.";
                                return RedirectToAction("RemoveRole", new { username = assignRole.UserName });
                            }
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "User does not have this role.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid Role Name.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid User Name.");
                }
            }

            // Re-populate ViewBag for dropdowns
            var users = UserManager.Users.ToList();
            var userRoles = users
                .Select(user => new
                {
                    UserName = user.UserName,
                    Roles = UserManager.GetRolesAsync(user).Result
                })
                .Where(ur => ur.Roles.Any())
                .ToList();

            ViewBag.Users = userRoles.Select(ur => new SelectListItem
            {
                Value = ur.UserName,
                Text = ur.UserName
            }).ToList();

            ViewBag.Roles = RoleManager.Roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name
            }).ToList();

            return View(assignRole);
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [Authorize]
        public async Task<IActionResult> SaveProfile(ApplicationUser editedUser)
        {
            // var user = await UserManager.FindByIdAsync(editedUser.Id);   
            var user = await UserManager.FindByNameAsync(editedUser.UserName);
            if (user == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                user.FirstName = editedUser.FirstName;
                user.LastName = editedUser.LastName;
                user.Email = editedUser.Email;
                user.NID = editedUser.NID;
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("Profile", user);
        }
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                if (user == null)
                {
                    return NotFound();
                }
                var result = await UserManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
                if (result.Succeeded)
                {
                    await SignInManager.RefreshSignInAsync(user);
                    ViewBag.Message = true;
                    return View();
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("ChangePassword", changePassword);
        }
        [Authorize]
        public async Task<IActionResult> DeleteRole(string roleName)
        {
            var role = await RoleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound();
            }
            var result = await RoleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("AddRole");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("AddRole");
        }


    }
}
