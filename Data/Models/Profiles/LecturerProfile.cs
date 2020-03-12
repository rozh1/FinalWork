using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;
using FinalWork_BD_Test.Data.Models.Data;

namespace FinalWork_BD_Test.Data.Models.Profiles
{

    /// <summary>
    /// Профиль научного руководителя
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
        /// Место работы
        /// </summary>
        [Required(ErrorMessage = "Введите место работы научного руководителя")]
        [Display(Name = "Место работы научного руководителя")]
        public string JobPlace { get; set; }

        /// <summary>
        /// Должность
        /// </summary>
        [Required(ErrorMessage = "Введите должность научного руководителя")]
        [Display(Name = "Должность научного руководителя")]
        public string JobPost { get; set; }
        
        [Display(Name = "Учёная степень")]
        public Guid? AcademicDegreeId { get; set; }
        /// <summary>
        /// Учёная степень
        /// </summary>
        [ForeignKey("AcademicDegreeId")]
        public virtual AcademicDegree AcademicDegree { get; set; }
        
        [Display(Name = "Учёное звание")]
        public Guid? AcademicTitleId { get; set; }
        /// <summary>
        /// Учёное звание
        /// </summary>
        [ForeignKey("AcademicTitleId")]
        public virtual AcademicTitle AcademicTitle { get; set; }

        [DefaultValue(false)]
        public bool IsArchived { get; set; }
    }
}
