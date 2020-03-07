using System.Collections.Generic;
using FinalWork_BD_Test.Data.Models.Profiles;

namespace FinalWork_BD_Test.Areas.Admin.Models
{
    public class ReviewerProfileViewModel
    {
        public IEnumerable<ReviewerProfile> Reviewers { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}