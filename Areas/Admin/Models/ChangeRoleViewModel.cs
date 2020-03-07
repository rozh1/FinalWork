using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace FinalWork_BD_Test.Models
{
    public class ChangeRoleViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public List<Role> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles = new List<Role>();
            UserRoles = new List<string>();
        }
    }
}
