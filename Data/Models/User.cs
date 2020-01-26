using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalWork_BD_Test.Data.Models
{
    public class User : IdentityUser<Guid>
    {
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

        public virtual ICollection<StudentProfile> StudentProfiles { get; set; } 
    }
}
