using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models.Profiles;

namespace FinalWork_BD_Test.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        public virtual ICollection<StudentProfile> StudentProfiles { get; set; } 
        public virtual ICollection<LecturerProfile> LecturerProfiles { get; set; }
    }
}
