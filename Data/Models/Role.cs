using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalWork_BD_Test.Data.Models
{
    public class Role : IdentityRole<Guid>
    {
        public Role() { }
        public Role(string name): base(name) { }
    }
}
