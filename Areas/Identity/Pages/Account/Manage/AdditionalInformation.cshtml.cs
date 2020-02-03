using System;
using System.Linq;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
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
        
        public IActionResult OnGet()
        {
            var currentUser = _userManager.GetUserAsync(this.User).Result;

            // Явная загрузка связанных данных, т.к они не подгружались неявно. 
            StudentProfile profile = _context.StudentProfiles
                .Include(profile => profile.Degree)
                .Include(profile => profile.Gender)
                .Include(profile => profile.EducationForm)
                .Include(profile => profile.GraduateSemester)
                .FirstOrDefault(t => t.User == currentUser && t.UpdatedByObj == null);

            if (profile == null)
            {
                ViewData["Degree"] = new SelectList(_context.Degrees.AsEnumerable(), "Name", "Name");
                ViewData["Gender"] = new SelectList(_context.Genders.AsEnumerable(), "Name", "Name");
                ViewData["EducationForm"] = new SelectList(_context.EducationForms.AsEnumerable(), "Name", "Name");
                ViewData["GraduateSemester"] = new SelectList(_context.Semesters.AsEnumerable(), "Name", "Name");
                return Page();
            }
            
            ViewData["Degree"] = new SelectList(_context.Degrees.AsEnumerable(), "Name", "Name", profile.Degree.Name);
            ViewData["Gender"] = new SelectList(_context.Genders.AsEnumerable(), "Name", "Name", profile.Gender.Name);
            ViewData["EducationForm"] = new SelectList(_context.EducationForms.AsEnumerable(), "Name", "Name", profile.EducationForm.Name);
            ViewData["GraduateSemester"] = new SelectList(_context.Semesters.AsEnumerable(), "Name", "Name", profile.GraduateSemester.Name);
            Input = profile;
            return Page();
        }
        
        public IActionResult OnPost([FromForm] StudentProfileView form)
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
                return RedirectToPage("AdditionalInformation");
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