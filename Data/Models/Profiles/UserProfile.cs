using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;

namespace FinalWork_BD_Test.Data.Models.Profiles
{
    /// <summary>
    /// Профиль пользователя.
    /// Хранит общие данные о пользователе.
    /// </summary>
    public class UserProfile : HistoricalModelBase<UserProfile>
    {
        /// <summary>
        /// Пользователь, кому принадлежит профиль
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Имя в именительном падеже
        /// </summary>
        [Required(ErrorMessage = "Введите имя в именительном падеже")]
        [Display(Name = "Имя в именительном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Имя в именительном падеже должно содержать только русские буквы")]
        public string FirstNameIP { get; set; }

        /// <summary>
        /// Фамилия в именительном падеже
        /// </summary>
        [Required(ErrorMessage = "Введите фамилию в именительном падеже")]
        [Display(Name = "Фамилия в именительном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Фамилия в именительном падеже должна содержать только русские буквы")]
        public string SecondNameIP { get; set; }

        /// <summary>
        /// Отчество в именительном падеже
        /// </summary>
        [Required(ErrorMessage = "Введите отчество в именительном падеже")]
        [Display(Name = "Отчество в именительном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Отчество в именительном падеже должно содержать только русские буквы")]
        public string MiddleNameIP { get; set; }

        [DefaultValue(false)]
        public bool IsArchived { get; set; }
    }
}
