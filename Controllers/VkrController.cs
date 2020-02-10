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

        public VkrController(ApplicationDbContext c, UserManager<User> u)
        {
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

            ViewData["Semester.Id"] = new SelectList(_context.Semesters.AsEnumerable(), "Id", "Name");
            ViewData["UserProfile.Id"] = GetSupervisorList();
            return View(new VKR { Year = (ulong)DateTime.Now.Year });
        }

        private SelectList GetSupervisorList(UserProfile supervisor = null)
        {
            Dictionary<Guid, string> dc = new Dictionary<Guid, string>();
            foreach (var userProfile in _context.UserProfiles.Where(t => t.UpdatedByObj == null))
            {
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
            UserProfile supervisor =
                _context.UserProfiles.FirstOrDefault(t => t.Id == userProfile.Id && t.UpdatedByObj == null);

            var currentUser = _userManager.GetUserAsync(this.User).Result;

            _context.UserProfiles.Load();

            VKR prvVKR = _context.VKRs
                .Include(t => t.Topic)
                .FirstOrDefault(t => t.UpdatedByObj == null);

            VKR vkr = new VKR()
            {
                Topic = topic,
                CreatedDate = DateTime.Now,
                SupervisorUP = supervisor,
                StudentUP = currentUser.UserProfiles.FirstOrDefault(t => t.UpdatedByObj == null),
                Year = year,
                SemesterId = semester.Id
            };



            //VKR beforeVkr = CreateBeforeVkr();

            // if (beforeVkr != null)
            if (prvVKR != null)
                if (EqualsVkr(prvVKR, vkr))
                    return RedirectToAction();

            _context.VKRs.Add(vkr);
            if (prvVKR != null)
                prvVKR.UpdatedByObj = vkr;

            var prvTopic = _context.Topics.FirstOrDefault(t => t.Author == currentUser && t.UpdatedByObj == null);

            topic.CreatedDate = DateTime.Now;
            topic.Author = currentUser;
            topic.UpdatedByObj = null;

            if (prvTopic != null)
                prvTopic.UpdatedByObj = topic;

            _context.Topics.Add(topic);
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
            if (beforeVkr.Topic.Title == afterVkr.Topic.Title && beforeVkr.SupervisorUP == afterVkr.SupervisorUP && beforeVkr.SemesterId == afterVkr.SemesterId && beforeVkr.Year == afterVkr.Year)
                return true;
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
        public IActionResult NewSuperVisor([FromForm] UserProfile user)
        {
            _context.UserProfiles.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Common");
        }
    }
}