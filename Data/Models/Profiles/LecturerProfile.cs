using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;
using FinalWork_BD_Test.Data.Models.Data;

namespace FinalWork_BD_Test.Data.Models.Profiles
{

    /// <summary>
    /// Профиль преподавателя
    /// </summary>
    public class LecturerProfile : HistoricalModelBase<LecturerProfile>
    {
        /// <summary>
        /// Пользователь, кому принадлежит профиль
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Имя в родительном падеже
        /// </summary>
        [Required(ErrorMessage = "Введите имя в родительном падеже")]
        [Display(Name = "Имя в родительном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Имя в родительном падеже должно содержать только русские буквы")]
        public string FirstNameRP { get; set; }

        /// <summary>
        /// Фамилия в родительном падеже
        /// </summary>
        [Required(ErrorMessage = "Введите фамилию в родительном падеже")]
        [Display(Name = "Фамилия в родительном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Фамилия в родительном падеже должна содержать только русские буквы")]
        public string SecondNameRP { get; set; }

        /// <summary>
        /// Отчество в родительном падеже
        /// </summary>
        [Required(ErrorMessage = "Введите отчество в родительном падеже")]
        [Display(Name = "Отчество в родительном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Отчество в родительном падеже должно содержать только русские буквы")]
        public string MiddleNameRP { get; set; }

        /// <summary>
        /// Имя в дательном падеже
        /// </summary>
        [Required(ErrorMessage = "Введите имя в дательном падеже")]
        [Display(Name = "Имя в дательном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Имя в дательном падеже должно содержать только русские буквы")]
        public string FirstNameDP { get; set; }

        /// <summary>
        /// Фамилия в дательном падеже
        /// </summary>
        [Required(ErrorMessage = "Введите фамилию в дательном падеже")]
        [Display(Name = "Фамилия в дательном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Фамилию в дательном падеже должна содержать только русские буквы")]
        public string SecondNameDP { get; set; }

        /// <summary>
        /// Отчество в дательном падеже
        /// </summary>
        [Required(ErrorMessage = "Введите отчество в дательном падеже")]
        [Display(Name = "Отчество в дательном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Отчество в дательном падеже должно содержать только русские буквы")]
        public string MiddleNameDP { get; set; }

        /// <summary>
        /// Учёная степень
        /// </summary>
        [Display(Name = "Учёная степень")]
        public Guid AcademicDegreeId { get; set; }
        [ForeignKey("AcademicDegreeId")]
        public virtual AcademicDegree AcademicDegree { get; set; }

        /// <summary>
        /// Учёное звание
        /// </summary>
        [Display(Name = "Учёное звание")]
        public Guid AcademicTitleId { get; set; }
        [ForeignKey("AcademicTitleId")]
        public virtual AcademicTitle AcademicTitle { get; set; }
    }
}
