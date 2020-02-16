using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Data;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalWork_BD_Test.Controllers
{
    [Authorize]
    public class VkrController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;

        public VkrController(ApplicationDbContext c, UserManager<User> u, RoleManager<Role> rm)
        {
            _roleManager = rm;
            _context = c;
            _userManager = u;
        }

        /// <summary>
        /// Общие сведения о ВКР
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Common()
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;
            
            var vkr = _context.VKRs
                .Include(t => t.StudentUP)
                .Include(t => t.SupervisorUP)
                .Include(t => t.Topic)
                .Include(t => t.Semester)
                .FirstOrDefault(t => t.StudentUP.User == currentUser && t.UpdatedByObj == null);

            ViewData["ActiveView"] = "Common";
            ViewData["CurrentYear"] = (ulong) DateTime.Now.Year;
            
            if (vkr != null)
            {
                ViewData["UserProfile.Id"] = VKR.GetSupervisorList(_context, _userManager, vkr.SupervisorUP);
                
                if (vkr.Semester == null)
                    vkr.Semester = _context.Semesters.First();
                
                ViewData["Semester.Id"] = new SelectList(_context.Semesters.AsEnumerable(), "Id", "Name", vkr.Semester.Id);
                
                return View(vkr);
            }

            ViewData["Semester.Id"] = new SelectList(_context.Semesters.AsEnumerable(),
                "Id", "Name", Semester.CurrentSemester(_context).Id);
            
            ViewData["UserProfile.Id"] = VKR.GetSupervisorList(_context, _userManager);
            
            return View(new VKR { Year = (ulong)DateTime.Now.Year });
        }


        /// <summary>
        /// Регистрация/редактирование ВКР
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Common([FromForm] Topic topic, [FromForm] UserProfile userProfile, [FromForm] ulong year, [FromForm] Semester semester)
        {
            //UserProfile supervisor =
            //    _context.UserProfiles.FirstOrDefault(t => t.Id == userProfile.Id && t.UpdatedByObj == null);
            
            var currentUser = _userManager.GetUserAsync(this.User).Result;

            topic.CreatedDate = DateTime.Now;
            topic.Author = currentUser;
            _context.Entry(currentUser).Collection(cu => cu.UserProfiles).Load();

            VKR prvVKR = _context.VKRs
                .Include(t => t.Topic)
                .FirstOrDefault(t => t.UpdatedByObj == null && t.StudentUP.User == currentUser);



            VKR vkr = new VKR()
            {
                Topic = topic,
                CreatedDate = DateTime.Now,
                SupervisorUPId = userProfile.Id,
                StudentUP = currentUser.UserProfiles.FirstOrDefault(t => t.UpdatedByObj == null),
                Year = year,
                SemesterId = semester.Id
            };
            
            if (prvVKR != null)
                if (VKR.EqualsVkr(prvVKR, vkr))
                    return RedirectToAction();

            _context.VKRs.Add(vkr);
            if (prvVKR != null)
                prvVKR.UpdatedByObj = vkr;

            _context.SaveChanges();

            return RedirectToAction();
        }
        
        
        public IActionResult Documents()
        {
            ViewData["ActiveView"] = "Documents";
            return View();
        }

        public IActionResult BuildVkr()
        {
            ViewData["ActiveView"] = "BuildVkr";
            return View();
        }

        /// <summary>
        /// Добавление нового научного руководителя
        /// </summary>
        /// <returns></returns>
        public IActionResult NewSuperVisor()
        {
            return View();
        }

        /// <summary>
        /// Добавление нового научного руководителя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult NewSuperVisor([FromForm] UserProfile userProfile)
        {
            userProfile.CreatedDate = DateTime.Now;
            _context.UserProfiles.Add(userProfile);

            /* Создать пользователя без имени можно, но не присвоится роль
             * Создаю пользователя с именем, созданого из guid профиля
             */
            User supervisor = new User { UserName = "null" };
            _userManager.CreateAsync(supervisor).Wait();

            userProfile.User = supervisor;
            _context.SaveChanges();

            _userManager.AddToRoleAsync(supervisor, "Supervisor").Wait();

            return RedirectToAction("Common");
        }
    }
}