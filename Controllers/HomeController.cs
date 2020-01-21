using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinalWork_BD_Test.Models;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace FinalWork_BD_Test.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)

        public ApplicationDbContext _context;
        private UserManager<User> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<User> userManager)
        {
            //_logger = logger;
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Начальная страница
        /// </summary>
        /// <returns> Возвращает представление </returns>
        public IActionResult Index()
        {
            //ViewBag.id = _context.
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        /// <summary>
        /// Страница добавления и редактирования тем
        /// </summary>
        /// <returns> Возвращает представление </returns>
        [HttpGet]
        [Authorize]
        public IActionResult Topic()
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;

            // Получаем тему
            var userTopic = _context.Topics.FirstOrDefault(t => t.Author == currentUser && t.UpdatedByObj == null);

            //  В зависимости от наличия у пользователя темы, создаем пустую или используем готовую модель для заполнения формы
            if (userTopic == null)
                return View("Topic"); // new Topic() ?
            else
                return View(userTopic);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Topic([FromForm] Topic topic)
        {
            
            var currentUser = _userManager.GetUserAsync(this.User).Result;
            
            // Получаем предыдущую тему, для обновления поля UpdatedBy
            var prvTopic = _context.Topics.FirstOrDefault(t => t.Author == currentUser && t.UpdatedByObj == null);

            // Заполняем поля темы
            topic.CreatedDate = DateTime.Now;
            topic.Author = currentUser;
            topic.UpdatedByObj = null;

            // И записываем topic в UpdatedBy 
            if (prvTopic != null)
               prvTopic.UpdatedByObj = topic;
            
            _context.Topics.Add(topic);
            _context.SaveChanges();
            return View();
        }
    }
}
