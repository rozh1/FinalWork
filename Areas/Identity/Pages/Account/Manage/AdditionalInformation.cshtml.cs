using System;
using System.Linq;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Profiles;
using FinalWork_BD_Test.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinalWork_BD_Test.Areas.Identity.Pages.Account.Manage
{
    public class AdditionalInformationModel : PageModel
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;

        public AdditionalInformationModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public StudentProfile Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult OnGet()
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;

            // Жадная загрузка связанных данных
            StudentProfile profile = _context.StudentProfiles
                .Include(profile => profile.Degree)
                .Include(profile => profile.Gender)
                .Include(profile => profile.EducationForm)
                .Include(profile => profile.GraduateSemester)
                .FirstOrDefault(t => t.User == currentUser && t.UpdatedByObj == null);

            if (profile == null)
            {
                ViewData["DegreeId"] = new SelectList(_context.Degrees.AsEnumerable(), "Id", "Name");
                ViewData["GenderId"] = new SelectList(_context.Genders.AsEnumerable(), "Id", "Name");
                ViewData["EducationFormId"] = new SelectList(_context.EducationForms.AsEnumerable(), "Id", "Name");
                ViewData["GraduateSemesterId"] = new SelectList(_context.Semesters.AsEnumerable(), "Id", "Name");

                return Page();
            }
            
            ViewData["DegreeId"] = new SelectList(_context.Degrees.AsEnumerable(), "Id", "Name", profile.Degree.Id);
            ViewData["GenderId"] = new SelectList(_context.Genders.AsEnumerable(), "Id", "Name", profile.Gender.Id);
            ViewData["EducationFormId"] = new SelectList(_context.EducationForms.AsEnumerable(), "Id", "Name", profile.EducationForm.Id);
            ViewData["GraduateSemesterId"] = new SelectList(_context.Semesters.AsEnumerable(), "Id", "Name", profile.GraduateSemester.Id);

            Input = profile;
            return Page();
        }
        
        public IActionResult OnPost([FromForm] StudentProfile formProfile)
        {
            User currentUser = _userManager.GetUserAsync(this.User).Result;

            // Явная загрузка, связанных с пользователем профилей
            _context.Entry(currentUser).Collection(c => c.StudentProfiles).Load();

            // Предыдущий профиль пользователя, необходим для связи в истории изменений
            StudentProfile prvProfile = currentUser.StudentProfiles.FirstOrDefault(up => up.UpdatedByObj == null);

            // Заполняем незаполненные ранее поля
            formProfile.CreatedDate = DateTime.Now;
            formProfile.User = currentUser;
            formProfile.UpdatedByObj = null;

            
            if (prvProfile != null)
                prvProfile.UpdatedByObj = formProfile;

            _context.StudentProfiles.Add(formProfile);
            _context.SaveChanges();

            StatusMessage = "Ваши данные были обновлены";

            return RedirectToPage();
        }
    }
}