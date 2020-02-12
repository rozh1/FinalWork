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
            if (vkr != null)
            {
                HttpContext.Session.SetString("beforeVkrTitle", vkr.Topic.Title);
                HttpContext.Session.SetString("beforeVkrSupervisor", vkr.SupervisorUP.Id.ToString());
                ViewData["UserProfile.Id"] = GetSupervisorList(vkr.SupervisorUP);
                if (vkr.Semester == null)
                    vkr.Semester = _context.Semesters.First();
                ViewData["Semester.Id"] = new SelectList(_context.Semesters.AsEnumerable(), "Id", "Name", vkr.Semester.Id);
                return View(vkr);
            }

            ViewData["Semester.Id"] = new SelectList(_context.Semesters.AsEnumerable(), "Id", "Name", CurrentSemester().Id);
            ViewData["UserProfile.Id"] = GetSupervisorList();
            return View(new VKR { Year = (ulong)DateTime.Now.Year });
        }

        private SelectList GetSupervisorList(UserProfile supervisor = null)
        {
            var users = _userManager.GetUsersInRoleAsync("Supervisor").Result;
            _context.UserProfiles.Load();

            Dictionary<Guid, string> dc = new Dictionary<Guid, string>();
            foreach (var user in users)
            {
                var userProfile = user.UserProfiles?.FirstOrDefault(up => up.UpdatedByObj == null);
                if (userProfile == null)
                    continue;
                dc.Add(userProfile.Id, $"{userProfile.SecondNameIP} {userProfile.FirstNameIP[0]}.{userProfile.MiddleNameIP[0]}.");
            }
            if (supervisor != null)
                return new SelectList(dc, "Key", "Value", supervisor.Id);
            return new SelectList(dc, "Key", "Value");
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
                if (EqualsVkr(prvVKR, vkr))
                    return RedirectToAction();

            _context.VKRs.Add(vkr);
            if (prvVKR != null)
                prvVKR.UpdatedByObj = vkr;

            _context.SaveChanges();

            return RedirectToAction();
        }

        /// <summary>
        /// Равны ли обе ВКР
        /// </summary>
        /// <param name="beforeVkr"></param>
        /// <param name="afterVkr"></param>
        /// <returns></returns>
        private bool EqualsVkr(VKR beforeVkr, VKR afterVkr)
        {
            if (beforeVkr.Topic.Title == afterVkr.Topic.Title)
            {
                afterVkr.Topic = beforeVkr.Topic;
                if (beforeVkr.SupervisorUPId == afterVkr.SupervisorUPId && beforeVkr.SemesterId == afterVkr.SemesterId &&
                    beforeVkr.Year == afterVkr.Year)
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Создает объект ВКР для сравнения с данными из формы
        /// </summary>
        /// <returns></returns>
        private VKR CreateBeforeVkr()
        {
            Topic topic = new Topic()
            {
                Title = HttpContext.Session.GetString("beforeVkrTitle")
            };

            Guid superVisorId = new Guid(HttpContext.Session.GetString("beforeVkrSupervisor"));

            UserProfile superVisor =
                _context.UserProfiles.FirstOrDefault(u => u.Id == superVisorId);

            VKR res = new VKR()
            {
                Topic = topic,
                SupervisorUP = superVisor
            };

            return res;
        }

        private Semester CurrentSemester()
        {
            int month = DateTime.Today.Month;
            if (month >= 2 && month <= 8)
                return _context.Semesters.FirstOrDefault(s => s.Name == "Весна");
            return _context.Semesters.FirstOrDefault(s => s.Name == "Осень");
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
            User supervisor = new User { UserName = userProfile.Id.ToString() };
            _userManager.CreateAsync(supervisor).Wait();

            userProfile.User = supervisor;
            _context.SaveChanges();

            _userManager.AddToRoleAsync(supervisor, "Supervisor").Wait();

            return RedirectToAction("Common");
        }
    }
}