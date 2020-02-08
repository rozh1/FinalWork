using System;
using System.Linq;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
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
    }
}