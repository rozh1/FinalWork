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
using FinalWork_BD_Test.Data.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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
            //topic.Supervisor = new User()
            //{
            //    FirstNameIP = topic.Supervisor.FirstNameIP,
            //    SecondNameIP = topic.Supervisor.SecondNameIP,
            //    MiddleNameIP = topic.Supervisor.MiddleNameIP
            //};
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

        [HttpGet]
        [Authorize]
        public IActionResult StudentProfile()
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;
            
            // Явная загрузка связанных данных, т.к они не подгружались неявно. 
            StudentProfile profile = _context.StudentProfiles
                .Include(profile => profile.Degree)
                .Include(profile => profile.Gender)
                .Include(profile => profile.EducationForm)
                .Include(profile => profile.GraduateSemester)
                .FirstOrDefault(t => t.User == currentUser && t.UpdatedByObj == null);

            
            ViewBag.Degree = new SelectList(_context.Degrees.AsEnumerable(), "Name", "Name", profile.Degree.Name);
            ViewBag.Gender = new SelectList(_context.Genders.AsEnumerable(), "Name", "Name", profile.Gender.Name);
            ViewBag.EducationForm = new SelectList(_context.EducationForms.AsEnumerable(), "Name", "Name", profile.EducationForm.Name);
            ViewBag.GraduateSemester = new SelectList(_context.Semesters.AsEnumerable(), "Name", "Name", profile.GraduateSemester.Name);
            
            if (profile == null)
                return View();
            else
                return View(profile);
        }

        [HttpPost]
        [Authorize]
        public IActionResult StudentProfile([FromForm] StudentProfileView form)
        {
            StudentProfile profile = Fill_profile_from_form(form);

            var currentUser = _userManager.GetUserAsync(this.User).Result;

            var prvProfile = _context.StudentProfiles.FirstOrDefault(t => t.User == currentUser && t.UpdatedByObj == null);

            profile.CreatedDate = DateTime.Now;
            profile.User = currentUser;
            profile.UpdatedByObj = null;

            if (prvProfile != null)
                prvProfile.UpdatedByObj = profile;

            _context.StudentProfiles.Add(profile);
            _context.SaveChanges();

            if (prvProfile == null)
                return RedirectToAction("Index", "Home");
            else
                //return View(profile);
                return RedirectToAction("StudentProfile", "Home");
        }

        private StudentProfile Fill_profile_from_form(StudentProfileView form)
        {
            StudentProfile profile = new StudentProfile
            {
                FirstNameRP = form.FirstNameRP,
                SecondNameRP = form.SecondNameRP,
                MiddleNameRP = form.MiddleNameRP,

                FirstNameDP = form.FirstNameDP,
                SecondNameDP = form.SecondNameDP,
                MiddleNameDP = form.MiddleNameDP,

                Degree = _context.Degrees.FirstOrDefault(d => d.Name == form.Degree),
                Gender = _context.Genders.FirstOrDefault(d => d.Name == form.Gender),
                EducationForm = _context.EducationForms.FirstOrDefault(d => d.Name == form.EducationForm),
                Group = form.Group,
                GraduateYear = form.GraduateYear,
                GraduateSemester = _context.Semesters.FirstOrDefault(d => d.Name == form.GraduateSemester),
            };

            return profile;
        }

    }
}
