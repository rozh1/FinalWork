using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FinalWork_BD_Test.Models;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.ConfigModels;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Data;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace FinalWork_BD_Test.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;
        private readonly IOptions<StaticFilesConfig> _config;

        public HomeController(ApplicationDbContext context, UserManager<User> userManager, IOptions<StaticFilesConfig> config)
        {
            _context = context;
            _userManager = userManager;
            _config = config;
        }

        /// <summary>
        /// Начальная страница
        /// </summary>
        /// <returns> Возвращает представление </returns>
        public IActionResult Index()
        {
            string textToPage = "";
            
            var file = Path.Combine(_config.Value.Path, "MainPage.txt");
            
            using (FileStream fl = new FileStream(file, FileMode.Open))
            {
                byte[] array = new byte[fl.Length];

                fl.Read(array, 0, array.Length);

                textToPage = System.Text.Encoding.Default.GetString(array);
            }

            ViewData["MainPageText"] = textToPage;
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpGet]
        [Authorize]
        public IActionResult StudentProfile()
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;

            // Явная загрузка связанных данных, т.к они не подгружались неявно. 
            StudentProfile profile = _context.StudentProfiles
                .Include(profile => profile.Gender)
                .Include(profile => profile.EducationForm)
                .FirstOrDefault(t => t.User == currentUser && t.UpdatedByObj == null);

            if (profile == null)
            {
                ViewBag.Gender = new SelectList(_context.Genders.AsEnumerable(), "Name", "Name");
                ViewBag.EducationForm = new SelectList(_context.EducationForms.AsEnumerable(), "Name", "Name");
                return View();
            }

            ViewBag.Gender = new SelectList(_context.Genders.AsEnumerable(), "Name", "Name", profile.Gender.Name);
            ViewBag.EducationForm = new SelectList(_context.EducationForms.AsEnumerable(), "Name", "Name", profile.EducationForm.Name);
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

                Gender = _context.Genders.FirstOrDefault(d => d.Name == form.Gender),
                EducationForm = _context.EducationForms.FirstOrDefault(d => d.Name == form.EducationForm),
                Group = form.Group,
            };

            return profile;
        }

        [HttpGet]
        [Authorize]
        public IActionResult LecturerProfile()
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;

            // Явная загрузка связанных данных, т.к они не подгружались неявно. 
            LecturerProfile profile = _context.LecturerProfiles
                .Include(profile => profile.AcademicDegree)
                .Include(profile => profile.AcademicTitle)
                .FirstOrDefault(t => t.User == currentUser && t.UpdatedByObj == null);

            if (profile == null)
            {
                ViewBag.AcademicDegreeId = new SelectList(_context.AcademicDegrees.AsEnumerable(), "Id", "Name");
                ViewBag.AcademicTitleId = new SelectList(_context.AcademicTitles.AsEnumerable(), "Id", "Name");
                return View();
            }

            ViewBag.AcademicDegreeId = new SelectList(_context.AcademicDegrees.AsEnumerable(), "Id", "Name", profile.AcademicDegree.Id);
            ViewBag.AcademicTitleId = new SelectList(_context.AcademicTitles.AsEnumerable(), "Id", "Name", profile.AcademicTitle.Id);
            return View(profile);
        }

        [HttpPost]
        [Authorize]
        public IActionResult LecturerProfile([FromForm] LecturerProfile profile)
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;

            _context.LecturerProfiles.Load();
            LecturerProfile prvProfile = currentUser.LecturerProfiles.FirstOrDefault(lp => lp.UpdatedByObj == null);

            profile.CreatedDate = DateTime.Now;
            profile.User = currentUser;
            profile.UpdatedByObj = null;

            if (prvProfile != null)
                prvProfile.UpdatedByObj = profile;

            _context.LecturerProfiles.Add(profile);
            _context.SaveChanges();
            return RedirectToAction("LecturerProfile", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult UserProfile()
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;
 
            var profile = _context.UserProfiles.FirstOrDefault(t => t.User == currentUser && t.UpdatedByObj == null);

            if (profile == null)
            {
                return View();
            }
            return View(profile);
        }

        [HttpPost]
        [Authorize]
        public IActionResult UserProfile([FromForm] UserProfile profile)
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;

            profile.CreatedDate = DateTime.Now;
            profile.User = currentUser;
            profile.UpdatedByObj = null;

            _context.UserProfiles.Load();
            UserProfile prvProfile;
            //var prvProfile = _context.LecturerProfiles.FirstOrDefault(t => t.User == currentUser && t.UpdatedByObj == null);
            if (currentUser.UserProfiles != null)
            {
                prvProfile = currentUser.UserProfiles.FirstOrDefault(lp => lp.UpdatedByObj == null);
                if (prvProfile != null)
                    prvProfile.UpdatedByObj = profile;
                currentUser.UserProfiles.Add(profile);
            }
            else
            {
                currentUser.UserProfiles = new List<UserProfile>();
                prvProfile = _context.UserProfiles.FirstOrDefault(t => t.User == currentUser && t.UpdatedByObj == null);
                if (prvProfile != null)
                    prvProfile.UpdatedByObj = profile;
                _context.UserProfiles.Add(profile);
                _context.SaveChanges();
            }
            return RedirectToAction("UserProfile", "Home");
        }

        public IActionResult Faq()
        {
            /* // 1 option - using FileStreamResult
            var file = Path.Combine(_config.Value.Path, "FaqPage.html");
            return new FileStreamResult(new FileStream(file, FileMode.Open), "text/html");
            */

            // 2 option - using Html.Raw in View
            string result;

            var file = Path.Combine(_config.Value.Path, "FaqPage.html");

            using (FileStream fl = new FileStream(file, FileMode.Open))
            {
                byte[] array = new byte[fl.Length];
                fl.Read(array, 0, array.Length);
                result = System.Text.Encoding.Default.GetString(array);
            }

            ViewData["page"] = result;

            return View();
        }
    }
}
