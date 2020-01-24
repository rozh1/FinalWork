﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalWork_BD_Test.Data.Models
{
    public class User : IdentityUser<Guid>
    {
        public virtual ICollection<StudentProfile> StudentProfiles { get; set; } 
    }
}
