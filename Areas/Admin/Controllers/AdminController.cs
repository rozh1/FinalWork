using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Areas.Admin.Models;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Data;
using FinalWork_BD_Test.Data.Models.Profiles;
using FinalWork_BD_Test.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalWork_BD_Test.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private UserManager<User> _userManager;
        private RoleManager<Role> _roleManager;

        public HomeController(ApplicationDbContext c, UserManager<User> u, RoleManager<Role> r)
        {
            _context = c;
            _userManager = u;
            _roleManager = r;
        }

        public IActionResult Index()
        {
            ViewData["ActiveView"] = "Index";
            return View();
        }

        public IActionResult AllUsers(int page=1)
        {
            int pageSize = 7;

            IQueryable<User> source = _context.Users.Include(profile => profile.UserProfiles);
            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
 
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            UserProfileViewModel viewModel = new UserProfileViewModel
            {
                PageViewModel = pageViewModel,
                Users = items
            };

            ViewData["ActiveView"] = "AllUsers";
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult EditUser(Guid id = default(Guid))
        {
            User profile = null;
            if (id != default(Guid))
            {
                profile = _context.Users
                    .Include(i => i.UserProfiles)
                    .Include(i => i.LecturerProfiles)
                    .Include(i => i.StudentProfiles)
                    .FirstOrDefault(u => u.Id == id);
            }

            UserProfile model = null;
            if (profile?.UserProfiles != null) 
                model = profile.UserProfiles.FirstOrDefault(u => u.UpdatedByObj == null);

            return model == null ? View() : View(model);
        }

        [HttpPost]
        public IActionResult EditUser([FromForm] UserProfile newProfile, [FromForm] string password)
        {
            // если редактирование
            var oldProfile = _context.UserProfiles
                .Include(u => u.User)
                .FirstOrDefault(u => u.Id == newProfile.Id && u.UpdatedByObj == null);

            if (oldProfile != null)
            {
                var changedUser = oldProfile.User;
                changedUser.Email = newProfile.User.Email;
                changedUser.PhoneNumber = newProfile.User.PhoneNumber;
                changedUser.UserName = newProfile.User.UserName;

                newProfile.User = changedUser;
                oldProfile.UpdatedByObj = newProfile;

                newProfile.Id = Guid.Empty;
                newProfile.CreatedDate = DateTime.Now;

                newProfile.User.UserProfiles.Add(newProfile);
                _context.UserProfiles.Add(newProfile);
            }
            else
            {
                // если добавление
                newProfile.CreatedDate = DateTime.Now;
                
                var newUser = newProfile.User;

                if (newUser.UserProfiles.Count > 0)
                    newUser.UserProfiles.Add(newProfile);
                else
                    newUser.UserProfiles = new List<UserProfile>()
                    {
                        newProfile
                    };
                
                _userManager.CreateAsync(newUser, password).Wait();
                _userManager.AddToRoleAsync(newUser, "Student").Wait();
            }

            _context.SaveChanges();

            var model = _context.UserProfiles
                .Include(u => u.User)
                .FirstOrDefault(u => u.Id == newProfile.Id);
            return View(model);
        }
        
        public IActionResult DeleteUser(Guid id)
        {
            // TODO: реализовать удаление пользователя (желательно просто деактивировать его)
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            //_userManager.DeleteAsync(user).Wait();
            
            _context.Entry(user).State = EntityState.Detached;
            _context.SaveChanges();

            return RedirectToAction("AllUsers");
        }

        public IActionResult AllRoles()
        {
            ViewData["roles"] = _roleManager.Roles.ToList();
            ViewData["ActiveView"] = "AllRoles";
            return View(_roleManager.Roles.ToList());
        }
        
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new Role(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("AllRoles");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("AllRoles");
        }
        
        public async Task<IActionResult> EditUserRoles(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }

            return NotFound();
        }
        
        [HttpPost]
        public async Task<IActionResult> EditUserRoles(string userId, List<string> roles)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(user, addedRoles);

                await _userManager.RemoveFromRolesAsync(user, removedRoles);

                return RedirectToAction("EditUserRoles", new {userId = userId});
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult EditLectureProfile(Guid id)
        {
            var profile = _context.LecturerProfiles
                .Include(u => u.User)
                .Include(u => u.AcademicDegree)
                .Include(u => u.AcademicTitle)
                .FirstOrDefault(u => u.User.Id == id && u.UpdatedByObj == null);

            if (profile != null)
            {
                ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "Id", "Name", profile.AcademicDegree.Id);
                ViewData["AcademicTitleId"] = new SelectList(_context.AcademicTitles, "Id", "Name", profile.AcademicTitle.Id);

                return View(profile);
            }
            else
            {
                var model = new LecturerProfile()
                {
                    User = new User(){ Id = id}
                };
                
                ViewData["AcademicDegreeId"] = new SelectList(_context.AcademicDegrees, "Id", "Name");
                ViewData["AcademicTitleId"] = new SelectList(_context.AcademicTitles, "Id", "Name");

                return View(model);
            }
        }

        [HttpPost]
        public IActionResult EditLectureProfile([FromForm] LecturerProfile newProfile)
        {
            var oldProfile = _context.LecturerProfiles
                .Include(u => u.User)
                .FirstOrDefault(u => u.Id == newProfile.Id && u.UpdatedByObj == null);

            if (oldProfile != null)
            {
                oldProfile.UpdatedByObj = newProfile;

                newProfile.Id = Guid.Empty;
                newProfile.User = oldProfile.User;
                newProfile.CreatedDate = DateTime.Now;
            }
            else
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == newProfile.User.Id);

                newProfile.User = user;
                newProfile.CreatedDate = DateTime.Now;

                _context.LecturerProfiles.Add(newProfile);
            }

            newProfile.User.LecturerProfiles.Add(newProfile);
            _context.SaveChanges();

            return RedirectToAction("EditLectureProfile", new {id = newProfile.User.Id});
        }
        
        [HttpGet]
        public IActionResult EditVkr(Guid id)
        {
            var vkr = _context.VKRs
                .Include(t => t.StudentUP)
                .Include(t => t.StudentUP.User)
                .Include(t => t.SupervisorUP)
                .Include(t => t.Topic)
                .Include(t => t.Semester)
                .FirstOrDefault(t => t.StudentUP.User.Id == id && t.UpdatedByObj == null);

            ViewData["CurrentYear"] = (ulong) DateTime.Now.Year;

            if (vkr != null)
            {
                ViewData["UserProfile.Id"] = VKR.GetSupervisorList(_context, _userManager, vkr.SupervisorUP);
                
                if (vkr.Semester == null)
                    vkr.Semester = _context.Semesters.First();
                
                ViewData["Semester.Id"] = new SelectList(_context.Semesters.AsEnumerable(), "Id", "Name", vkr.Semester.Id);

                return View(vkr);
            }
            else
            {
                ViewData["Semester.Id"] = new SelectList(_context.Semesters.AsEnumerable(), 
                    "Id", "Name", Semester.CurrentSemester(_context).Id);
                
                ViewData["UserProfile.Id"] = VKR.GetSupervisorList(_context, _userManager);
                
                var studentUp = new UserProfile()
                {
                    User = new User(){Id = id}
                };
                return View(new VKR { Year = (ulong)DateTime.Now.Year, StudentUP = studentUp});
            }
        }

        [HttpPost]
        public IActionResult EditVkr([FromForm] Topic topic, [FromForm] UserProfile userProfile,
            [FromForm] ulong year, [FromForm] Semester semester, [FromForm] Guid userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);

            topic.CreatedDate = DateTime.Now;
            topic.Author = user;
            _context.Entry(user).Collection(cu => cu.UserProfiles).Load();

            var prvVKR = _context.VKRs
                .Include(t => t.Topic)
                .FirstOrDefault(t => t.UpdatedByObj == null && t.StudentUP.User == user);

            var vkr = new VKR()
            {
                Topic = topic,
                CreatedDate = DateTime.Now,
                SupervisorUPId = userProfile.Id,
                StudentUP = user.UserProfiles.FirstOrDefault(t => t.UpdatedByObj == null),
                Year = year,
                SemesterId = semester.Id
            };

            if (prvVKR != null)
            {
                if (VKR.EqualsVkr(prvVKR, vkr))
                    return RedirectToAction("EditVkr", new {id = user.Id});

                prvVKR.UpdatedByObj = vkr;
            }

            _context.VKRs.Add(vkr);
            _context.SaveChanges();

            return RedirectToAction("EditVkr", new {id = user.Id});
        }
        
        [HttpGet]
        public IActionResult EditStudentProfile(Guid id)
        {
            var profile = _context.StudentProfiles
                .Include(p => p.User)
                .Include(p => p.Degree)
                .Include(p => p.Gender)
                .Include(p => p.EducationForm)
                .FirstOrDefault(t => t.User.Id == id && t.UpdatedByObj == null);

            if (profile != null)
            {
                ViewData["DegreeId"] = new SelectList(_context.Degrees.AsEnumerable(), "Id", "Name", profile.Degree.Id);
                ViewData["GenderId"] = new SelectList(_context.Genders.AsEnumerable(), "Id", "Name", profile.Gender.Id);
                ViewData["EducationFormId"] = new SelectList(_context.EducationForms.AsEnumerable(), "Id", "Name", profile.EducationForm.Id);

                return View(profile);
            }
            else
            {
                var model = new StudentProfile()
                {
                    User = new User(){ Id = id}
                };
                
                ViewData["DegreeId"] = new SelectList(_context.Degrees.AsEnumerable(), "Id", "Name");
                ViewData["GenderId"] = new SelectList(_context.Genders.AsEnumerable(), "Id", "Name");
                ViewData["EducationFormId"] = new SelectList(_context.EducationForms.AsEnumerable(), "Id", "Name");

                return View(model);
            }
        }

        [HttpPost]
        public IActionResult EditStudentProfile([FromForm] StudentProfile newProfile)
        {
            var oldProfile = _context.StudentProfiles
                .Include(u => u.User)
                .FirstOrDefault(u => u.Id == newProfile.Id && u.UpdatedByObj == null);

            if (oldProfile != null)
            {
                oldProfile.UpdatedByObj = newProfile;

                newProfile.Id = Guid.Empty;
                newProfile.User = oldProfile.User;
                newProfile.CreatedDate = DateTime.Now;
            }
            else
            {
                var user = _context.Users.FirstOrDefault(u => u.Id == newProfile.User.Id);

                newProfile.User = user;
                newProfile.CreatedDate = DateTime.Now;

                _context.StudentProfiles.Add(newProfile);
            }

            newProfile.User.StudentProfiles.Add(newProfile);
            _context.SaveChanges();

            return RedirectToAction("EditStudentProfile", new {id = newProfile.User.Id});
        }
    }
}