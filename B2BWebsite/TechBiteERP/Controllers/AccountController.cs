using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TechBiteERP.Data;
using TechBiteERP.Model.Models;
using TechBiteERP.Model.ViewModels;

namespace TechBiteERP.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _dbContext;
        const string SessionName = "_Name";
        const string SessionAge = "_Age";
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext DbContext, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = DbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            // Prevents application to load default query string on login page(?ReturnUrl=2F)
            ViewBag.FinancialYear = new SelectList (  _dbContext.FinYear.ToList() ,"Id","FYear" );
            if (string.IsNullOrEmpty(Request.QueryString.Value))
                return View();
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var customer = (from c in _dbContext.ArCustomer.Where(x=>x.CustomerType==3)
                             select new HREmployee()
                             {
                                 EmployeeID = c.Id,
                                 EmployeeName = c.Name
                             }).ToList();

            var employees = _dbContext.HREmployees.ToList();
            ViewBag.Employees =new SelectList( employees.Concat(customer), "EmployeeID", "EmployeeName");
            ViewBag.Role = new SelectList(_dbContext.Roles, "Name", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser
                //{
                //    UserName = model.UserName,
                //    Email = model.Email,
                //};
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, FullName = model.FullName, Photo = model.Photo, Id = model.Id, CompanyId = 1 ,UserType =model.UserType };
                user.Id = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, model.UserRoles);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Dashboard");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(model);
        }
        public IActionResult EditRegisteredUser(string id)
        {
            var customer = (from c in _dbContext.ArCustomer.Where(x => x.CustomerType == 3)
                            select new HREmployee()
                            {
                                EmployeeID = c.Id,
                                EmployeeName = c.Name
                            }).ToList();

            var employees = _dbContext.HREmployees.ToList();
            ViewBag.Employees = new SelectList(employees.Concat(customer), "EmployeeID", "EmployeeName");
            ViewBag.RoleName = new SelectList(_dbContext.Roles//.Where(u => !u.Name.Contains("Admin"))
                      .ToList(), "Name", "Name");
            var RoleId = (from r in _dbContext.UserRoles.Where(x => x.UserId == id) select r.RoleId).FirstOrDefault();
            var model = (from u in _dbContext.Users.Where(x => x.Id == id)
                         select new RegisterViewModel
                         {
                             Id = u.Id,
                             FullName = u.FullName,
                             UserName = u.UserName,
                             UserRoles = RoleId,
                             Email =u.Email,
                             UserType=u.UserType
                            
                         }).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRegisteredUser(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    FullName = registerViewModel.FullName,
                    Photo = registerViewModel.Photo,
                    Id = registerViewModel.Id,
                    UserType = registerViewModel.UserType
                };
                var result = await _userManager.UpdateAsync(user);
            }
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.NavbarHeading = "Change Password";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["error"] = "true";
                TempData["message"] = "Please Enter Correct Password.";
                return RedirectToAction(nameof(ChangePassword));
            }
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.ConfirmNewPassword);
            if (result.Succeeded)
            {
                TempData["error"] = "false";
                TempData["message"] = "You have Successfully Changed Your Password...";
            }
            else
            {
                TempData["error"] = "true";
                TempData["message"] = "Something went wrong while changing Password.";
            }
            return RedirectToAction(nameof(ChangePassword));
        }
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult Login()
        //{
        //    return View();
        //}
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user,IFormCollection collection)
        {
            var companyName = HttpContext.Session.GetString("CompanyName");
            if (user.UserName == "Sajid" && user.Password == "786")
            {
                var identity = new ClaimsIdentity(new[]{
                    new Claim(ClaimTypes.Name,user.UserName)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, user.RememberMe, false);
                    
                    if (result.Succeeded)
                    {
                        string userId = user.UserId;
                        string userName = user.UserName;
                        string companyNam = Convert.ToString(_dbContext.ApplicationCompanies.Where(x => x.Id == 1).FirstOrDefault().Name);
                          var companyUser =  _dbContext.Users.Where(x => x.UserName == user.UserName).FirstOrDefault();
                        string BranchPhoto = Convert.ToString(_dbContext.Branch.Where(x => x.ID == 1).FirstOrDefault().Photo);
                        HttpContext.Session.SetString("CompanyName", companyNam);
                        HttpContext.Session.SetString("UserId", companyUser.Id?? ""  );
                        HttpContext.Session.SetString("UserName", "" + userName ??"" + "");
                        HttpContext.Session.SetInt32("BranchId", 1);  
                        HttpContext.Session.SetInt32("CompanyId", 1 );  
                        HttpContext.Session.SetInt32("FinancialYearId",Convert.ToInt32(collection["FinancialYear"]) );  
                        HttpContext.Session.SetString("BranchPhoto", BranchPhoto);  
                        return RedirectToAction("Index", "Dashboard");
                    }
                    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
                }
            }
            return View(user);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        //[Authorize(Roles = "Manager, Admin, Developer, Administrator")]
        [Authorize]
        public IActionResult Index()
        {
            
            var users = User.Identities.ToList();
            var allusers = _dbContext.Users.ToList();
            var userVM = allusers.Select(user => new UserViewModel
            { Id=user.Id ,  Username = user.UserName, Email = user.Email}).ToList();
            var model = userVM;
            return View(model);

        }

        public IActionResult CreateRole()
        {
            return View();
        }

        public IActionResult RoleIndex()
        {
            var allusers = _dbContext.Roles.ToList();
            var userVM = allusers.Select(role => new RoleViewModel
            { RoleId = role.Id, RoleName = role.Name }).ToList();
            var model = userVM;
            return View(model);

        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel roleViewModel)
        {
            //var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await _roleManager.RoleExistsAsync(roleViewModel.RoleName);
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await _roleManager.CreateAsync(new IdentityRole(roleViewModel.RoleName));
                return RedirectToAction(nameof(Index));
            }
            else
                ViewBag.Message = "Role already exists.";
            return View();
        }

        [HttpGet]
        public IActionResult GetMenus(string roleId)
        {
             
            var menus = from m in _dbContext.ApplicationMenu
                        select new
                        {
                            m.Id,
                            m.Name,
                            m.ParentId,
                            m.IconClass,
                            Check = (_dbContext.ApplicationRoleMenus
                                        .Where(r => r.MenuId.Equals(m.Id) && r.RoleId.Equals(roleId))
                                        .Select(r => r.Id)
                                        ).FirstOrDefault()
                        };


            IList<TreeMenu> treeMenu = new TreeMenu[menus.Count()];
            int i = 0;
            foreach (var item in menus)
            {
                TreeMenu menu = new TreeMenu();
                menu.Id = item.Id.ToString();
                menu.Text = item.Name;
                menu.Icon = item.IconClass;
                menu.Parent = (item.ParentId == 0 ? "#" : item.ParentId.ToString());
                //menu.State.Selected = false;
                TreeMenuState state = new TreeMenuState();
                if (item.Check == 0)
                    state.Selected = false;
                else
                    state.Selected = true;
                menu.State = state;
                treeMenu[i] = menu;
                i++;
            }
            return Ok(treeMenu);
        }

        [HttpGet]
        public IActionResult BindRole(string id)
        {
            RoleViewModel role = new RoleViewModel();
            role.RoleId = id;

            //Show role name at front end assign role window
            ViewBag.RoleName = _dbContext.Roles
                .Where(r => r.Id == id)
                .Select(r => r.Name).FirstOrDefault();

            return View(role);
        }
        [HttpPost]
        public IActionResult BindRole(RoleViewModel roleView)
        {
            //delete old roles and insert new
            var roles = _dbContext.ApplicationRoleMenus.Where(r => r.RoleId.Equals(roleView.RoleId));
            _dbContext.ApplicationRoleMenus.RemoveRange(roles);
            //insert new roles
            string[] menuId = roleView.Menus.Split(',');
            foreach (string id in menuId)
            {
                ApplicationRoleMenu menu = new ApplicationRoleMenu();
                menu.MenuId = Convert.ToInt32(id);
                menu.RoleId = roleView.RoleId;
                _dbContext.ApplicationRoleMenus.Add(menu);

            }

            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));

        }

        public class TreeMenu
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public string Parent { get; set; }
            public string Icon { get; set; }
            public TreeMenuState State { get; set; }
        }
        public class TreeMenuState
        {
            public bool Selected { get; set; }
        }
    }
}
