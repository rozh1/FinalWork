using System;
using System.Linq;
using FinalWork_BD_Test.Areas.Admin.Models;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Profiles;
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

        public HomeController(ApplicationDbContext c, UserManager<User> u)
        {
            _context = c;
            _userManager = u;
        }

        public IActionResult Index()
        {
            ViewData["ActiveView"] = "Index";
            return View();
        }

        public IActionResult AllUsers(int page=1)
        {
            int pageSize = 10;

            IQueryable<UserProfile> source = _context.UserProfiles;
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
 
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            UserProfileViewModel viewModel = new UserProfileViewModel
            {
                PageViewModel = pageViewModel,
                UserProfiles = items
            };

            ViewData["ActiveView"] = "AllUsers";
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EditUser(Guid id = default(Guid))
        {
            UserProfile profile = null;
            if (id != default(Guid))
                profile = _context.UserProfiles.FirstOrDefault(u => u.Id == id);

            return profile == null ? View() : View(profile);
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
            var user = _context.UserProfiles.FirstOrDefault(u => u.Id == id)?.User;
            if (user != null)
                foreach (var profile in user.UserProfiles)
                    _context.UserProfiles.Remove(profile);

            return RedirectToAction("AllUsers");
        }
    }
}