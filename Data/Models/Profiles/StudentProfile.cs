using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string FirstNameRP { get; set; }

        /// <summary>
        /// Фамилия в родительном падеже
        /// </summary>
        public string SecondNameRP { get; set; }

        /// <summary>
        /// Отчество в родительном падеже
        /// </summary>
        public string MiddleNameRP { get; set; }

        /// <summary>
        /// Имя в дательном падеже
        /// </summary>
        public string FirstNameDP { get; set; }

        /// <summary>
        /// Фамилия в дательном падеже
        /// </summary>
        public string SecondNameDP { get; set; }

        /// <summary>
        /// Отчество в дательном падеже
        /// </summary>
        public string MiddleNameDP { get; set; }

        /// <summary>
        /// Степень образования
        /// </summary>
        public Degree Degree { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Форма обучения 
        /// </summary>
        public EducationForm EducationForm { get; set; }

        /// <summary>
        /// Группа
        /// </summary>
        [MaxLength(5)]
        public string Group { get; set; } // Думаю стоит хранить в строке, но при этом ограничить её длину

        /// <summary>
        /// Год выпуска
        /// </summary>
        public ushort GraduateYear { get; set; } // Или же в DateTime?

        /// <summary>
        /// Семестр выпуска
        /// </summary>
        public Semester GraduateSemester { get; set; }

        //место работы(или место предполагаемой работы) после выпуска
        //public string WorkPlace {get; set;}
    }
}
