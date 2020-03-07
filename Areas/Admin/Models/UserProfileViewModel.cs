using System.Collections.Generic;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Profiles;

namespace FinalWork_BD_Test.Areas.Admin.Models
{
    public class UserProfileViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}