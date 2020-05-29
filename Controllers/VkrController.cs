using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.ConfigModels;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Data;
using FinalWork_BD_Test.Data.Models.Profiles;
using FinalWork_BD_Test.Documents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FinalWork_BD_Test.Controllers
{
    [Authorize]
    public class VkrController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;
        private IOptions<DocumentsConfig> _documentsConfig;

        public VkrController(ApplicationDbContext c, UserManager<User> u, RoleManager<Role> rm, IOptions<DocumentsConfig> documentsConfig)
        {
            _roleManager = rm;
            _context = c;
            _userManager = u;
            _documentsConfig = documentsConfig;
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
                .Include(t => t.Degree)
                .Include(t => t.Topic)
                .Include(t => t.Semester)
                .Include(t => t.ReviewerUP)
                .FirstOrDefault(t => t.StudentUP.User == currentUser && t.UpdatedByObj == null);

             
            ViewData["ActiveView"] = "Common";
            ViewData["CurrentYear"] = (ulong) DateTime.Now.Year;
            
            if (vkr != null)
            {
                ViewData["UserProfile.Id"] = VKR.GetSupervisorList(_context, _userManager, vkr.SupervisorUP);
                
                if (vkr.Semester == null)
                    vkr.Semester = _context.Semesters.First();
                
                ViewData["Semester.Id"] = new SelectList(_context.Semesters.AsEnumerable(), 
                    "Id", "Name", vkr.Semester.Id);
                
                ViewData["Degree.Id"] = new SelectList(_context.Degrees.AsEnumerable(), 
                    "Id", "Name", vkr.DegreeId);
                
                ViewData["ReviewerId"] = ReviewerProfile.GetReviewerList(_context, vkr.ReviewerUP);

                return View(vkr);
            }
            
            ViewData["Degree.Id"] = new SelectList(_context.Degrees.AsEnumerable(), 
                "Id", "Name");
            
            ViewData["Semester.Id"] = new SelectList(_context.Semesters.AsEnumerable(), 
                "Id", "Name", Semester.CurrentSemester(_context).Id);
            
            ViewData["UserProfile.Id"] = VKR.GetSupervisorList(_context, _userManager);
            
            ViewData["ReviewerId"] = ReviewerProfile.GetReviewerList(_context);
            
            return View(new VKR { Year = (ulong)DateTime.Now.Year });
        }


        /// <summary>
        /// Регистрация/редактирование ВКР
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Common([FromForm] Topic topic, [FromForm] UserProfile userProfile, [FromForm] ulong year, 
            [FromForm] Semester semester, [FromForm] Guid? reviewerId, [FromForm] Degree degree)
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;

            topic.CreatedDate = DateTime.Now;
            topic.Author = currentUser;

            _context.Entry(currentUser).Collection(cu => cu.UserProfiles).Load();
            
            //var superVisorLp = userProfile.User.LecturerProfiles.FirstOrDefault(l => l.UpdatedByObj == null);
            var superVisorLp = _context.LecturerProfiles.FirstOrDefault(l =>
                l.User.UserProfiles.FirstOrDefault(u => u.UpdatedByObj == null).Id == userProfile.Id);

            var prvVkr = _context.VKRs
                .Include(t => t.Topic)
                .FirstOrDefault(t => t.UpdatedByObj == null && t.StudentUP.User == currentUser);

            if (_context.Degrees.FirstOrDefault(d => d.Id == degree.Id)?.Name != "Магистр")
                reviewerId = null;

            var vkr = new VKR()
            {
                Topic = topic,
                CreatedDate = DateTime.Now,
                SupervisorUPId = userProfile.Id,
                SupervisorLPId = superVisorLp?.Id,
                StudentUP = currentUser.UserProfiles.FirstOrDefault(t => t.UpdatedByObj == null),
                Year = year,
                SemesterId = semester.Id,
                ReviewerUPId = reviewerId,
                DegreeId = degree.Id
            };
            
            if (prvVkr != null)
                if (VKR.EqualsVkr(prvVkr, vkr))
                    return RedirectToAction();

            _context.VKRs.Add(vkr);

            if (prvVkr != null)
                prvVkr.UpdatedByObj = vkr;

            _context.SaveChanges();

            return RedirectToAction();
        }
        
        public IActionResult DocumentsForms()
        {
            ViewData["ActiveView"] = "DocumentsForms";
            ViewData["DocumentsFormsDictionary"] = new Dictionary<string, string>
            {
                //Название шаблона, имя файла шаблона
                { "Приложение 1.Титульный лист", "Приложение_1.Титульный_лист"},
                { "Приложение 2.Бланк задания на ВКР", "Приложение_2.Бланк_задания_на_ВКР"},
                { "Приложение 3.Бланк акта предварительной защиты ВКР", "Приложение_3.Бланк_акта_предварительной_защиты_ВКР"},
                { "Приложение 4.Бланк отзыва руководителя на ВКР", "Приложение_4.Бланк_отзыва_руководителя_на_ВКР"},
                { "Приложение 5.Бланк рецензии на ВКР", "Приложение_5.Бланк_рецензии_на_ВКР"},
                { "Согласие размещение текста ВКР в ЭБС КНИТУ-КАИ", "Согласие_размещение_текста_ВКР_в_ЭБС_КНИТУ-КАИ"}
            };
            return View();
        }
        public IActionResult MainDocuments()
        {
            ViewData["ActiveView"] = "MainDocuments";
            var currentUser = _userManager.GetUserAsync(this.User).Result;
            VKR vkr = _context.VKRs
                .Include(vkr => vkr.StudentUP)
                .Include(vkr => vkr.UploadableDocuments).ThenInclude(ud => ud.LocalizedStatus)
                .FirstOrDefault(vkr => vkr.StudentUP.User == currentUser && vkr.UpdatedBy == null);

            var documentsList = new List<string>
            {
                "Полностью собранная ВКР",
                "Отзыв научного руководителя",
            };

            var documents = new Dictionary<string, UploadableDocument>();

            foreach (var document in documentsList)
            {
                documents.Add(document,
                    vkr?.UploadableDocuments.FirstOrDefault(ud => ud.Type == document && ud.UpdatedByObj == null));
            }

            ViewData["MainDocumentsDictionary"] = documents;
            

            return View();
        }

        public FileResult DownloadMainDocument(Guid id)
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;
            var document = _context.UploadableDocuments.FirstOrDefault(ud => ud.Id == id);

            var fileResult = new PhysicalFileResult( document.Path, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = $"{document.OriginalName}"
            };

            return fileResult;
        }

        public IActionResult OtherDocuments()
        {
            ViewData["ActiveView"] = "OtherDocuments";
            return View();
        }

        public IActionResult BuildVkr()
        {
            ViewData["ActiveView"] = "BuildVkr";
            return View();
        }

        public IActionResult Documents()
        {
            ViewData["ActiveView"] = "Documents";
            return View();
        }

        public FileResult Generate(string templateName)
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;
            return Generator.Generate(templateName, _context, currentUser, _documentsConfig);
        }

        public IActionResult UploadDocument(IFormFile uploadedDocument, string type)
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;
            var currentVkr = _context.VKRs
                .Include(vkr => vkr.UploadableDocuments)
                .FirstOrDefault(vkr =>
                vkr.IsArchived == false && vkr.UpdatedBy == null && vkr.StudentUP.User == currentUser);

            if (uploadedDocument != null)
            {
                var previousDocument = currentVkr.UploadableDocuments.FirstOrDefault(ud => ud.Type == type);

                UploadableDocument uploadableDocument = new UploadableDocument
                {
                    Type = type,
                    OriginalName = uploadedDocument.FileName, 
                    Path = "", 
                    Length = uploadedDocument.Length, 
                    Status = DocumentStatus.Verification, 
                    CreatedDate = DateTime.Now,
                    Vkr = currentVkr,
                    UpdatedByObj = previousDocument
                };
                _context.UploadableDocuments.Add(uploadableDocument);

                var directoryPath = $"{_documentsConfig.Value.UploadsPath}/{uploadableDocument.Type}";
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                uploadableDocument.Path = $"{directoryPath}/{uploadableDocument.Id}_{uploadedDocument.FileName}";


                using (var fileStream = new FileStream(uploadableDocument.Path, FileMode.Create))
                {
                    uploadedDocument.CopyTo(fileStream);
                }

                _context.SaveChanges();
            }

            return RedirectToAction("MainDocuments");
        }

        /// <summary>
        /// Добавление нового научного руководителя
        /// </summary>
        /// <returns></returns>
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
    }
}