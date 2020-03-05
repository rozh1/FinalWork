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
    /// Профиль рецензента
    /// </summary>
    public class ReviewerProfile : HistoricalModelBase<ReviewerProfile>
    {
        [Required(ErrorMessage = "Введите имя в именительном падеже")]
        [Display(Name = "Имя в именительном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Имя в именительном падеже должно содержать только русские буквы")]
        public string FirstNameIP { get; set; }

        [Required(ErrorMessage = "Введите фамилию в именительном падеже")]
        [Display(Name = "Фамилия в именительном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Фамилия в именительном падеже должна содержать только русские буквы")]
        public string SecondNameIP { get; set; }

        [Required(ErrorMessage = "Введите отчество в именительном падеже")]
        [Display(Name = "Отчество в именительном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Отчество в именительном падеже должно содержать только русские буквы")]
        public string MiddleNameIP { get; set; }

        [Required(ErrorMessage = "Введите место работы рецензента")]
        [Display(Name = "Место работы рецензента")]
        public string JobPlace { get; set; }

        [Required(ErrorMessage = "Введите должность рецензента")]
        [Display(Name = "Должность рецензента")]
        public string JobPost { get; set; }

        [Display(Name = "Учёное звание (при наличии)")]
        public Guid? AcademicTitleId { get; set; }
        [ForeignKey("AcademicTitleId")]
        public virtual AcademicTitle AcademicTitle { get; set; }

        [Display(Name = "Учёная степень (при наличии)")]
        public Guid? AcademicDegreeId { get; set; }
        [ForeignKey("AcademicDegreeId")]
        public virtual AcademicDegree AcademicDegree { get; set; }

        [DefaultValue(false)]
        public bool IsArchived { get; set; }
    }
}
