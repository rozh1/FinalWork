using System;
using System.Collections.Generic;
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
        public string FirstNameIP { get; set; }

        /// <summary>
        /// Фамилия в именительном падеже
        /// </summary>
        public string SecondNameIP { get; set; }

        /// <summary>
        /// Отчество в именительном падеже
        /// </summary>
        public string MiddleNameIP { get; set; }
    }
}
