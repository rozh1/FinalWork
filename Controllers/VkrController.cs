using System;
using System.Linq;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            var userTopic = _context.Topics.FirstOrDefault(t => t.Author == currentUser && t.UpdatedByObj == null);

            ViewData["ActiveView"] = "Common";

            if (userTopic == null)
            {
                return View();
            }
            else
            {
                ViewData["beforeTopic"] = userTopic;
                return View(userTopic);
            }
        }
        
        /// <summary>
        /// Регистрация/редактирование ВКР
        /// </summary>
        /// <param name="topic"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Common([FromForm] Topic topic)
        {
            if (EqualsTopics((Topic) ViewData["beforeTopic"], topic))
                return RedirectToAction();

            var currentUser = _userManager.GetUserAsync(this.User).Result;
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
                if (beforeTopic.SuperVisorId == afterTopic.SuperVisorId)
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