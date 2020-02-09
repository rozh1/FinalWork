using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.AspNetCore.Authorization;
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
                .Include(t => t)
                .FirstOrDefault(t => t.StudentUP.User == currentUser && t.UpdatedByObj == null);

            ViewData["ActiveView"] = "Common";
            if (vkr != null)
            {
                //ViewData["beforeTopic"] = userTopic;
                ViewData["UserProfile.Id"] = GetSupervisorList(vkr.SupervisorUP);
                return View(vkr);
            }

            ViewData["UserProfile.Id"] = GetSupervisorList();
            return View();
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
        /// <returns></returns>
        [HttpPost]
        public IActionResult Common([FromForm] Topic topic, [FromForm] UserProfile userProfile)
        {
            if (ViewData["beforeTopic"] != null)
                if (EqualsTopics((Topic)ViewData["beforeTopic"], topic))
                    return RedirectToAction();
            UserProfile supervisor =
                _context.UserProfiles.FirstOrDefault(t => t.Id == userProfile.Id && t.UpdatedByObj == null);
            var currentUser = _userManager.GetUserAsync(this.User).Result;
            _context.UserProfiles.Load();
            VKR prvVKR = _context.VKRs.FirstOrDefault(t => t.UpdatedByObj == null);
            VKR vkr = new VKR() { Topic = topic, CreatedDate = DateTime.Now, SupervisorUP = supervisor, StudentUP = currentUser.UserProfiles.FirstOrDefault(t => t.UpdatedByObj == null) };
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
        /// <param name="beforeTopic"></param>
        /// <param name="afterTopic"></param>
        /// <returns></returns>
        private bool EqualsTopics(Topic beforeTopic, Topic afterTopic)
        {
            if (beforeTopic.Title == afterTopic.Title)
                //if (beforeTopic.SuperVisorId == afterTopic.SuperVisorId)
                return true;

            return false;
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