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
        /// Регситрация тем
        /// </summary>
        /// <param name="topic"> Данные из формы </param>
        [HttpPost]
        [Authorize]
        public async void RegisterTopic(Topic topic)
        {
            // узнаем данные текущего авторизованного пользователя
            var curUser = await _userManager.GetUserAsync(this.User);

            // заполняем недостающие поля
            topic.CreatedDate = DateTime.Now;
            topic.UpdatedBy = curUser.Id;

            // добавляем запись в БД и сохраняем изменения
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновление ланных тем
        /// </summary>
        /// <param name="topic"> Данные из формы </param>
        [HttpPost]
        [Authorize]
        public async void UpdateTopic(Topic topic)
        {
            // узнаем данные текущего авторизованного пользователя
            var curUser = await _userManager.GetUserAsync(this.User);

            // заполняем недостающие поля
            topic.CreatedDate = DateTime.Now;
            topic.UpdatedBy = curUser.Id;

            // достаем запись со старыми данными из БД
            Topic oldTopic = _context.Topics.Where(t => t.Id == topic.Id).ToList()[0] as Topic;

            // добавляем данные о новой записи
            oldTopic.UpdatedByObj = topic;

            // добавляем запись в БД и сохраняем изменения
            _context.Update(oldTopic);
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();
        }
    }
}
