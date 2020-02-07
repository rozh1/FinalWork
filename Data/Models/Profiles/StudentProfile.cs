﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;
using FinalWork_BD_Test.Data.Models.Data;

namespace FinalWork_BD_Test.Data.Models
{
    /// <summary>
    /// Профиль студента
    /// </summary>
    public class StudentProfile : HistoricalModelBase<StudentProfile>
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
            ErrorMessage = "Фамилия в дательном падеже должна содержать только русские буквы")]
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
        /// Степень образования
        /// </summary>
        [Required(ErrorMessage = "Выберите степень образования")]
        [Display(Name = "Степень образования")]
        public Guid DegreeId { get; set; }
        [ForeignKey("DegreeId")]
        public virtual Degree Degree { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        [Required(ErrorMessage = "Выберите пол")]
        [Display(Name = "Пол")]
        public Guid GenderId { get; set; }
        [ForeignKey("GenderId")]
        public virtual Gender Gender { get; set; }

        /// <summary>
        /// Форма обучения 
        /// </summary>
        [Required(ErrorMessage = "Выберите форму обучения")]
        [Display(Name = "Форма обучения")]
        public Guid EducationFormId { get; set; }
        [ForeignKey("EducationFormId")]
        public EducationForm EducationForm { get; set; }

        /// <summary>
        /// Группа
        /// </summary>
        [Required(ErrorMessage = "Введите номер группы")]
        [Display(Name = "Номер группы")]
        [RegularExpression(@"[1-9 0]+", 
            ErrorMessage = "Номер группы должен содержать только цифры")]
        [MaxLength(5)]
        public string Group { get; set; } // Думаю стоит хранить в строке, но при этом ограничить её длину

        /// <summary>
        /// Год выпуска
        /// </summary>
        [Required(ErrorMessage = "Введите год окончания")]
        [Display(Name = "Год окончания")]
        [RegularExpression(@"[1-9 0]+", 
            ErrorMessage = "Год окончания должен содержать только цифры")]
        public ushort GraduateYear { get; set; } // Или же в DateTime?

        /// <summary>
        /// Семестр выпуска
        /// </summary>
        [Required(ErrorMessage = "Выберите семестр, когда будет окончание")]
        [Display(Name = "Семестр, когда будет окончание")]
        public Guid GraduateSemesterId { get; set; }
        [ForeignKey("GraduateSemesterId")]
        public Semester GraduateSemester { get; set; }

        //место работы(или место предполагаемой работы) после выпуска
        //public string WorkPlace {get; set;}
    }
}
