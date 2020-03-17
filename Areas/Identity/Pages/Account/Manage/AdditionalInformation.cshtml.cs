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
                .Include(profile => profile.Gender)
                .Include(profile => profile.EducationForm)
                .FirstOrDefault(t => t.User == currentUser && t.UpdatedByObj == null);

            if (profile == null)
            {
                ViewData["GenderId"] = new SelectList(_context.Genders.AsEnumerable(), "Id", "Name");
                ViewData["EducationFormId"] = new SelectList(_context.EducationForms.AsEnumerable(), "Id", "Name");

                return Page();
            }

            ViewData["GenderId"] = new SelectList(_context.Genders.AsEnumerable(), "Id", "Name", profile.Gender.Id);
            ViewData["EducationFormId"] = new SelectList(_context.EducationForms.AsEnumerable(), "Id", "Name", profile.EducationForm.Id);

            Input = profile;
            return Page();
        }

        public IActionResult OnPost([FromForm] StudentProfile formProfile)
        {
            User currentUser = _userManager.GetUserAsync(this.User).Result;

            // Предыдущий профиль пользователя, необходим для связи в истории изменений
            StudentProfile prvProfile = _context.StudentProfiles.FirstOrDefault(up => up.UpdatedByObj == null && up.User == currentUser);

            if (prvProfile != null)
            {
                prvProfile.UpdatedByObj = formProfile;
                if (Equals(prvProfile, formProfile))
                {
                    StatusMessage = "Изменения не обнаружены";
                    return RedirectToPage();
                }
            }

            // Заполняем незаполненные ранее поля
            formProfile.CreatedDate = DateTime.Now;
            formProfile.User = currentUser;

            _context.StudentProfiles.Add(formProfile);
            _context.SaveChanges();

            StatusMessage = "Ваши данные были обновлены";

            return RedirectToPage();
        }

        // Если будет использоваться где-то еще, то стоит перенести в модель
        private static bool Equals(StudentProfile previous, StudentProfile current)
        {
            if (previous.FirstNameDP == current.FirstNameDP
                && previous.SecondNameDP == current.SecondNameDP
                && previous.MiddleNameDP == current.MiddleNameDP
                && previous.FirstNameRP == current.FirstNameRP
                && previous.SecondNameRP == current.SecondNameRP
                && previous.MiddleNameRP == current.MiddleNameRP
                && previous.EducationFormId == current.EducationFormId
                && previous.GenderId == current.GenderId
                && previous.Group == current.Group)
                return true;
            return false;
        }

    }
}