using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;
using FinalWork_BD_Test.Data.Models.Data;
using FinalWork_BD_Test.Data.Models.Intermidate;

namespace FinalWork_BD_Test.Data.Models.Profiles
{
    public class GecMemberProfile : HistoricalModelBase<GecMemberProfile>
    {
        public User User { get; set; }

        public virtual ICollection<GecMemberIntermediate> GECs { get; set; }

        [Required(ErrorMessage = "Введите имя в именительном падеже")]
        [Display(Name = "Имя в именительном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+( [А-Яа-яЁё]+)*(-[А-Яа-яЁё]+)*(`[А-Яа-яЁё]+)*('[А-Яа-яЁё]+)*",
            ErrorMessage = "Имя в именительном падеже может содержать русские буквы, пробел, а также спец символы: -, `, '")]
        public string FirstNameIP { get; set; }

        [Required(ErrorMessage = "Введите фамилию в именительном падеже")]
        [Display(Name = "Фамилия в именительном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+( [А-Яа-яЁё]+)*(-[А-Яа-яЁё]+)*(`[А-Яа-яЁё]+)*('[А-Яа-яЁё]+)*",
            ErrorMessage = "Фамилия в именительном падеже может содержать русские буквы, пробел, а также спец символы: -, `, '")]
        public string SecondNameIP { get; set; }

        [Display(Name = "Отчество в именительном падеже")]
        [RegularExpression(@"[А-Яа-яЁё]+",
            ErrorMessage = "Отчество в именительном падеже должно содержать только русские буквы")]
        public string MiddleNameIP { get; set; }

        [Required(ErrorMessage = "Введите место работы члена ГЭК")]
        [Display(Name = "Место работы члена ГЭК")]
        public string JobPlace { get; set; }

        [Required(ErrorMessage = "Введите должность члена ГЭК")]
        [Display(Name = "Должность члена ГЭК")]
        public string JobPost { get; set; }

        [Display(Name = "Учёное звание (при наличии)")]
        public Guid? AcademicTitleId { get; set; }
        [ForeignKey("AcademicTitleId")]
        public virtual AcademicTitle AcademicTitle { get; set; }

        [Display(Name = "Учёная степень (при наличии)")]
        public Guid? AcademicDegreeId { get; set; }
        [ForeignKey("AcademicDegreeId")]
        public virtual AcademicDegree AcademicDegree { get; set; }

        //ToDo: Change queries to use this field
        [DefaultValue(false)]
        public bool IsArchived { get; set; }
    }
}
