using System;
using System.Linq;
using FinalWork_BD_Test.Areas.Admin.Models;
using FinalWork_BD_Test.Areas.Admin.Pages;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalWork_BD_Test.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;

        public AdminController(ApplicationDbContext c, UserManager<User> u)
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

        public IActionResult EditUser(Guid id)
        {
            return View();
        }

        public IActionResult DeleteUser()
        {
            throw new System.NotImplementedException();
        }
    }
}