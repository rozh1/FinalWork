using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Base;
using FinalWork_BD_Test.Data.Models.Profiles;

namespace FinalWork_BD_Test.Data.Models
{
    public class VKR : HistoricalModelBase<VKR>
    {
        /* Есть множество способов обеспечить связь с данными
         * от хранения ссылок на профили, момента последнего обновления
         * так определение профиля от времени обновления и пользователя
         */
        //public User Student { get; set; }
        public UserProfile StudentUP { get; set; }
        public Topic Topic { get; set; }
        //public User Supervisor { get; set; }
        public UserProfile SupervisorUP { get; set; }
        //public User Reviewer { get; set; }
    }
}
