using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinalWork_BD_Test.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private ApplicationDbContext _context;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [Display(Name = "Логин")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Номер телефона")]
            public string PhoneNumber { get; set; }
            
            [Required(ErrorMessage = "Введите фамилию в именительном падеже")]
            [Display(Name = "Фамилия в именительном падеже")]
            [RegularExpression(@"[А-Яа-яЁё]+", 
                ErrorMessage = "Фамилия в именительном падеже должна содержать только русские буквы")]
            public string SecondNameIP { get; set; }
            
            [Required(ErrorMessage = "Введите имя в именительном падеже")]
            [Display(Name = "Имя в именительном падеже")]
            [RegularExpression(@"[А-Яа-яЁё]+", 
                ErrorMessage = "Имя в именительном падеже должно содержать только русские буквы")]
            public string FirstNameIP { get; set; }
            
            [Required(ErrorMessage = "Введите отчество в именительном падеже")]
            [Display(Name = "Отчество в именительном падеже")]
            [RegularExpression(@"[А-Яа-яЁё]+", 
                ErrorMessage = "Отчество в именительном падеже должно содержать только русские буквы")]
            public string MiddleNameIP { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            var u = _context.Users
                .Include(us => us.UserProfiles)
                .FirstOrDefault(us => us.Id == user.Id);

            UserProfile userProfile = null;
            if (u != null)
                userProfile = u.UserProfiles
                    .FirstOrDefault(p => p.UpdatedByObj == null);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };

            if (userProfile != null)
            {
                Input.FirstNameIP = userProfile.FirstNameIP;
                Input.SecondNameIP = userProfile.SecondNameIP;
                Input.MiddleNameIP = userProfile.MiddleNameIP;
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не удалось загрузить пользователя с ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не удалось загрузить пользователя с ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            /*var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    var userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }*/
            
            User currentUser = _userManager.GetUserAsync(this.User).Result;

            var oldProfile = _context.UserProfiles
                .Include(u => u.User)
                .FirstOrDefault(u => u.User.Id == currentUser.Id && u.UpdatedByObj == null);
            
            var newProfile = new UserProfile()
            {
                FirstNameIP = Input.FirstNameIP,
                SecondNameIP = Input.SecondNameIP,
                MiddleNameIP = Input.MiddleNameIP
            };

            if (oldProfile != null)
            {
                var changedUser = oldProfile.User;
                changedUser.PhoneNumber = Input.PhoneNumber;

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

                var newUser = _context.Users
                    .Include(u => u.UserProfiles)
                    .FirstOrDefault(u => u.Id == currentUser.Id);

                if (newUser.UserProfiles.Count > 0)
                    newUser.UserProfiles.Add(newProfile);
                else
                    newUser.UserProfiles = new List<UserProfile>()
                    {
                        newProfile
                    };
            }


            //await _signInManager.RefreshSignInAsync(user);
            _context.SaveChanges();
            StatusMessage = "Ваш профиль был обновлен";
            return RedirectToPage();
        }
    }
}
