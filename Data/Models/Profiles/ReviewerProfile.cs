using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;
using FinalWork_BD_Test.Data.Models.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinalWork_BD_Test.Data.Models.Profiles
{
    /// <summary>
    /// Профиль рецензента
    /// </summary>
    public class ReviewerProfile : HistoricalModelBase<ReviewerProfile>
    {
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
        
        public static IEnumerable<SelectListItem> GetReviewerList(ApplicationDbContext context, ReviewerProfile reviewer = null)
        {
            Dictionary<Guid, string> dc = new Dictionary<Guid, string>();
            foreach (var reviewerProfile in context.ReviewerProfiles.Include(rp => rp.AcademicTitle).Include(rp => rp.AcademicDegree).Where(rp => rp.UpdatedByObj == null && !rp.IsArchived))
            {
                dc.Add(reviewerProfile.Id, $"{reviewerProfile.AcademicTitle?.Name} {reviewerProfile.AcademicDegree?.Name} {reviewerProfile.SecondNameIP} {reviewerProfile.FirstNameIP[0]}.{reviewerProfile.MiddleNameIP?[0]}.");
            }

            return new SelectList(dc, "Key", "Value", reviewer?.Id).Append(new SelectListItem("", "null", reviewer == null));
        }

    }
}
