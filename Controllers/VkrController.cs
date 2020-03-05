using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
                .Include(t => t.ReviewerUP)
                .FirstOrDefault(t => t.StudentUP.User == currentUser && t.UpdatedByObj == null);

             
            ViewData["ActiveView"] = "Common";
            ViewData["CurrentYear"] = (ulong) DateTime.Now.Year;
            if (vkr != null)
            {
                HttpContext.Session.SetString("beforeVkrTitle", vkr.Topic.Title);
                HttpContext.Session.SetString("beforeVkrSupervisor", vkr.SupervisorUP.Id.ToString());
                ViewData["UserProfile.Id"] = GetSupervisorList(vkr.SupervisorUP);
                ViewData["ReviewerId"] = GetReviewerList(vkr.ReviewerUP);
                if (vkr.Semester == null)
                    vkr.Semester = _context.Semesters.First();
                ViewData["Semester.Id"] = new SelectList(_context.Semesters.AsEnumerable(), "Id", "Name", vkr.Semester.Id);
                ViewData["Degree.id"] = new SelectList(_context.Degrees.AsEnumerable(), "Id", "Name", vkr.DegreeId);
                return View(vkr);
            }
            ViewData["Degree.id"] = new SelectList(_context.Degrees.AsEnumerable(), "Id", "Name");
            ViewData["Semester.Id"] = new SelectList(_context.Semesters.AsEnumerable(), "Id", "Name", CurrentSemester().Id);
            ViewData["UserProfile.Id"] = GetSupervisorList();
            ViewData["ReviewerId"] = GetReviewerList();
            return View(new VKR { Year = (ulong)DateTime.Now.Year });
        }

        private SelectList GetSupervisorList(UserProfile supervisor = null)
        {
            var users = _userManager.GetUsersInRoleAsync("Supervisor").Result;
            _context.UserProfiles.Load();
            _context.LecturerProfiles.Include(lp => lp.AcademicTitle).Include(lp => lp.AcademicDegree).Load();

            Dictionary<Guid, string> dc = new Dictionary<Guid, string>();
            foreach (var user in users)
            {
                var userProfile = user.UserProfiles?.FirstOrDefault(up => up.UpdatedByObj == null);
                if (userProfile == null)
                    continue;
                var lecturerProfile = userProfile.User?.LecturerProfiles?.FirstOrDefault(lp => lp.UpdatedByObj == null);

                string dropdowntext;
                if (lecturerProfile != null)
                    dropdowntext = $"{(lecturerProfile.AcademicTitle != null ? lecturerProfile.AcademicTitle.Name : "")} {(lecturerProfile.AcademicDegree != null ? lecturerProfile.AcademicDegree.Name : "")} {userProfile.SecondNameIP} {userProfile.FirstNameIP[0]}.{userProfile.MiddleNameIP[0]}.";
                else
                    dropdowntext = $"{userProfile.SecondNameIP} {userProfile.FirstNameIP[0]}.{userProfile.MiddleNameIP[0]}.";
                dc.Add(userProfile.Id, dropdowntext);
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
        public IActionResult Common([FromForm] Topic topic, [FromForm] UserProfile userProfile, [FromForm] ulong year, [FromForm] Semester semester, [FromForm] Guid? reviewerId, [FromForm] Degree degree)
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;

            topic.CreatedDate = DateTime.Now;
            topic.Author = currentUser;

            _context.Entry(currentUser).Collection(cu => cu.UserProfiles).Load();

            VKR prvVKR = _context.VKRs
                .Include(t => t.Topic)
                .FirstOrDefault(t => t.UpdatedByObj == null && t.StudentUP.User == currentUser);

            if (_context.Degrees.FirstOrDefault(d => d.Id == degree.Id)?.Name != "Магистр")
                reviewerId = null;

            VKR vkr = new VKR()
            {
                Topic = topic,
                CreatedDate = DateTime.Now,
                SupervisorUPId = userProfile.Id,
                StudentUP = currentUser.UserProfiles.FirstOrDefault(t => t.UpdatedByObj == null),
                Year = year,
                SemesterId = semester.Id,
                ReviewerUPId = reviewerId,
                DegreeId = degree.Id
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
        private bool EqualsVkr(VKR beforeVkr, VKR afterVkr)
        {
            if (beforeVkr.Topic.Title == afterVkr.Topic.Title)
            {
                afterVkr.Topic = beforeVkr.Topic;
                if (beforeVkr.StudentUPId == afterVkr.StudentUPId
                    && beforeVkr.StudentSPId == afterVkr.StudentSPId
                    && beforeVkr.SupervisorUPId == afterVkr.SupervisorUPId
                    && beforeVkr.SupervisorLPId == afterVkr.SupervisorLPId
                    && beforeVkr.ReviewerUPId == afterVkr.ReviewerUPId
                    && beforeVkr.Year == afterVkr.Year
                    && beforeVkr.SemesterId == afterVkr.SemesterId
                    && beforeVkr.DegreeId == afterVkr.DegreeId
                    )
                    return true;
            }

            return false;
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
        public IActionResult NewSuperVisor()
        {
            ViewData["Item2.AcademicDegreeId"] = new SelectList(_context.AcademicDegrees.AsNoTracking().AsEnumerable(), "Id", "Name").Append(new SelectListItem("Отсутствует", "null", true));
            ViewData["Item2.AcademicTitleId"] = new SelectList(_context.AcademicTitles.AsNoTracking().AsEnumerable(), "Id", "Name").Append(new SelectListItem("Отсутствует", "null", true));

            return View();
        }
        
        /// <summary>
        /// Добавление нового научного руководителя
        /// </summary>
        [HttpPost]
        public IActionResult NewSuperVisor([FromForm] UserProfile item1, [FromForm] LecturerProfile item2)
        {
            var userProfile = item1;
            var lecturerProfile = item2;

            userProfile.CreatedDate = DateTime.Now;
            _context.UserProfiles.Add(userProfile);

            lecturerProfile.CreatedDate = DateTime.Now;

            /* Хм, надо заполнять поля ФИО в падежах, чтобы записать в БД.
            *  Либо поля делать nullable
            */
            lecturerProfile.FirstNameDP = "";
            lecturerProfile.FirstNameRP = ""; 
            lecturerProfile.SecondNameDP = "";
            lecturerProfile.SecondNameRP = "";
            lecturerProfile.MiddleNameDP = "";
            lecturerProfile.MiddleNameRP = "";

            _context.LecturerProfiles.Add(lecturerProfile);
            

            /* Создать пользователя без имени можно, но не присвоится роль
             * Создаю пользователя с именем, созданого из guid профиля
             */
            User supervisor = new User { UserName = userProfile.Id.ToString() };
            _userManager.CreateAsync(supervisor).Wait();

            userProfile.User = supervisor;
            lecturerProfile.User = supervisor;
            _context.SaveChanges();

            _userManager.AddToRoleAsync(supervisor, "Supervisor").Wait();

            return RedirectToAction("Common");
        }

        [HttpGet]
        public IActionResult NewReviewer()
        {
            ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees.AsNoTracking().AsEnumerable(), "Id", "Name").Append(new SelectListItem("Отсутствует", "null", true));
            ViewData["AcademicTitleId"] = new SelectList(_context.AcademicTitles.AsNoTracking().AsEnumerable(), "Id", "Name").Append(new SelectListItem("Отсутствует", "null", true));

            return View();
        }

        [HttpPost]
        public IActionResult NewReviewer([FromForm] ReviewerProfile reviewerProfile)
        {
            _context.ReviewerProfiles.Add(reviewerProfile);
            _context.SaveChanges();
            return RedirectToAction("Common");
        }

        private IEnumerable<SelectListItem> GetReviewerList(ReviewerProfile reviewer = null)
        {
            Dictionary<Guid, string> dc = new Dictionary<Guid, string>();
            foreach (var reviewerProfile in _context.ReviewerProfiles.Include(rp => rp.AcademicTitle).Include(rp => rp.AcademicDegree).Where(rp => rp.UpdatedByObj == null && !rp.IsArchived))
            {
                dc.Add(reviewerProfile.Id, $"{reviewerProfile.AcademicTitle?.Name} {reviewerProfile.AcademicDegree?.Name} {reviewerProfile.SecondNameIP} {reviewerProfile.FirstNameIP[0]}.{reviewerProfile.MiddleNameIP[0]}.");
            }

            return new SelectList(dc, "Key", "Value", reviewer?.Id).Append(new SelectListItem("", "null", reviewer == null));
        }
    }
}