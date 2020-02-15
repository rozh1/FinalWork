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

            IQueryable<User> source = _context.Users.Include(profile => profile.UserProfiles);
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
            {
                profile = _context.Users
                    .Include(i => i.UserProfiles)
                    .Include(i => i.LecturerProfiles)
                    .Include(i => i.StudentProfiles)
                    .FirstOrDefault(u => u.Id == id);
            }

            UserProfile model = null;
            if (profile?.UserProfiles != null) model = profile.UserProfiles.FirstOrDefault();

            return model == null ? View() : View(model);
        }

        [HttpPost]
        public IActionResult EditUser([FromForm] UserProfile newProfile)
        {
            if (newProfile.Id != Guid.Empty)
            {
                // если редактирование
                var oldProfile = _context.UserProfiles
                    .Include(u => u.User)
                    .FirstOrDefault(u => u.Id == newProfile.Id);

                if (oldProfile != null)
                {
                    var changedUser = oldProfile.User;
                    changedUser.Email = newProfile.User.Email;
                    changedUser.PhoneNumber = newProfile.User.PhoneNumber;

                    newProfile.User = changedUser;
                    oldProfile.UpdatedByObj = newProfile;

                    newProfile.Id = Guid.Empty;
                    newProfile.CreatedDate = DateTime.Now;

                    _context.UserProfiles.Add(newProfile);
                }
            }
            else
            {
                // если добавление
                newProfile.CreatedDate = DateTime.Now;
                
                var newUser = newProfile.User;
                newUser.UserName = newUser.Email;
                
                newUser.UserProfiles = new List<UserProfile>()
                {
                    newProfile
                };
                
                string pass = newUser.PasswordHash;
                newUser.PasswordHash = null;

                _userManager.CreateAsync(newUser, pass).Wait();
                _userManager.AddToRoleAsync(newUser, "Student").Wait();
            }

            _context.SaveChanges();

            var model = _context.UserProfiles
                .Include(u => u.User)
                .FirstOrDefault(u => u.Id == newProfile.Id);
            return View(model);
        }

        private void UpdateInfo<T>(Guid id, T newProfile, DbSet<T> dbSet) where T : User
        {
            var oldProfile = dbSet.FirstOrDefault(u => u.Id == id);
        }
    
        public IActionResult DeleteUser(Guid id)
        {
            // TODO: реализовать удаление пользователя (желательно просто деактивировать его)
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            //_userManager.DeleteAsync(user).Wait();
            
            _context.Entry(user).State = EntityState.Detached;
            _context.SaveChanges();

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
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
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
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("EditUserRoles", new {userId = userId});
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult EditLectureProfile(Guid id)
        {
            var profile = _context.LecturerProfiles
                .Include(u => u.User)
                .FirstOrDefault(u => u.Id == id);

            return View(profile);
        }

        [HttpPost]
        public IActionResult EditLectureProfile([FromForm] LecturerProfile newProfile)
        {
            var oldProfile = _context.LecturerProfiles
                .Include(u => u.User)
                .FirstOrDefault(u => u.Id == newProfile.Id);

            if (oldProfile != null)
            {
                oldProfile.UpdatedByObj = newProfile;

                newProfile.User = oldProfile.User;
                newProfile.CreatedDate = DateTime.Now;
            }
            else
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == newProfile.User.Id);

                newProfile.User = user;
                newProfile.CreatedDate = DateTime.Now;
            }

            _context.SaveChanges();

            return RedirectToAction("EditLectureProfile", new {id = newProfile.User.Id});
        }
        
        // TODO: сделать редактирование профиля студента аналогично профилю науч рука
    }
}