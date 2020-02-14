using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Areas.Admin.Models;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Profiles;
using FinalWork_BD_Test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalWork_BD_Test.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;

        public HomeController(ApplicationDbContext c, UserManager<User> u, RoleManager<Role> r)
        {
            _context = c;
            _userManager = u;
            _roleManager = r;
        }

        public IActionResult Index()
        {
            ViewData["ActiveView"] = "Index";
            return View();
        }

        public IActionResult AllUsers(int page=1)
        {
            int pageSize = 10;

            IQueryable<User> source = _context.Users;
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
 
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            UserProfileViewModel viewModel = new UserProfileViewModel
            {
                PageViewModel = pageViewModel,
                Users = items
            };

            ViewData["ActiveView"] = "AllUsers";
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EditUser(Guid id = default(Guid))
        {
            User profile = null;
            if (id != default(Guid))
                profile = _context.Users.FirstOrDefault(u => u.Id == id);

            UserProfile model = null;
            if (profile?.UserProfiles != null) model = profile.UserProfiles.FirstOrDefault();

            return model == null ? View() : View(model);
        }

        [HttpPost]
        public IActionResult EditUser([FromForm] UserProfile newProfile)
        {
            if (newProfile.Id != Guid.Empty)
            {
                if (newProfile.User != null)
                    _context.Users.Update(newProfile.User);

                _context.UserProfiles.Update(newProfile);
            }
            else
            {
                _context.Users.Add(newProfile.User);
                
                var newUserId = _context.Find<User>(newProfile.User).Id;
                newProfile.User.Id = newUserId;

                _context.Add(newProfile);
            }

            _context.SaveChanges();

            var model = _context.Find<UserProfile>(newProfile);
            return View(model);
        }

        private void UpdateInfo<T>(Guid id, T newProfile, DbSet<T> dbSet) where T : User
        {
            var oldProfile = dbSet.FirstOrDefault(u => u.Id == id);
        }
    
        public IActionResult DeleteUser(Guid id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            _userManager.DeleteAsync(user);

            return RedirectToAction("AllUsers");
        }

        public IActionResult AllRoles()
        {
            ViewData["roles"] = _roleManager.Roles.ToList();
            ViewData["ActiveView"] = "AllRoles";
            return View(_roleManager.Roles.ToList());
        }
        
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new Role(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("AllRoles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("AllRoles");
        }
        
        public async Task<IActionResult> EditUserRoles(string userId)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> EditUserRoles(string userId, List<string> roles)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(user);
                // получаем все роли
                var allRoles = _roleManager.Roles.ToList();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction();
            }

            return NotFound();
        }

    }
}