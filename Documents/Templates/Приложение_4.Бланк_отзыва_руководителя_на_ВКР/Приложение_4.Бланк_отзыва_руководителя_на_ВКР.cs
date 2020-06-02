using System.Linq;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemplateEngine.Docx;

namespace FinalWork_BD_Test.Documents
{
    public static partial class Generator
    {
        private static Content GenerateHeadReviewContent(User user)
        {
            _context.Entry(user).Collection(x => x.UserProfiles).Load();
            var userUP = user.UserProfiles.FirstOrDefault(x => x.UpdatedByObj == null);
            
            _context.Entry(user).Collection(x => x.StudentProfiles).Load();
            var userSP = user.StudentProfiles.FirstOrDefault(x => x.UpdatedByObj == null);

            if (userUP == null && userSP == null)
                return null;

            var vkr = _context.VKRs
                .Include(l => l.Topic)
                .Include(l => l.SupervisorUP)
                .Include(l => l.SupervisorLP.AcademicDegree)
                .Include(l => l.SupervisorLP.AcademicTitle)
                .FirstOrDefault(x => x.UpdatedByObj == null && x.StudentUPId == userUP.Id);

            if (vkr == null)
                return null;

            var content = new Content(
                new FieldContent("topic", vkr.Topic.Title),
                new FieldContent("student", $"{userSP.SecondNameRP} " +
                                            $"{userSP.FirstNameRP} " +
                                            $"{userSP.MiddleNameRP} "),
                new FieldContent("degree", vkr.SupervisorLP.AcademicDegree.Name.ToLower()),
                new FieldContent("title", vkr.SupervisorLP.AcademicTitle.Name.ToLower()),
                new FieldContent("supervisor",
                    $"{vkr.SupervisorUP.SecondNameIP} {vkr.SupervisorUP.FirstNameIP} {vkr.SupervisorUP.MiddleNameIP}")
            ); 
            
            return content;
        }

    }
}