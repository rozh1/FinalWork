using System;
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
        private static Content GenerateReviewContent(User user)
        {
            _context.Entry(user).Collection(x => x.UserProfiles).Load();
            var userUP = user.UserProfiles.FirstOrDefault(x => x.UpdatedByObj == null);
            
            _context.Entry(user).Collection(x => x.StudentProfiles).Load();
            var userSP = user.StudentProfiles.FirstOrDefault(x => x.UpdatedByObj == null);

            if (userUP == null && userSP == null)
                return null;

            var vkr = _context.VKRs
                .Include(l => l.Topic)
                .Include(l => l.ReviewerUP)
                //.Include(l => l.SupervisorLP.AcademicDegree)
                //.Include(l => l.SupervisorLP.AcademicTitle)
                .FirstOrDefault(x => x.UpdatedByObj == null && x.StudentUPId == userUP.Id);

            if (vkr == null || vkr.ReviewerUP == null)
                return null;

            var content = new Content(
                new FieldContent("topic", vkr.Topic.Title),
                new FieldContent("student", $"{userSP.SecondNameRP} " +
                                            $"{userSP.FirstNameRP} " +
                                            $"{userSP.MiddleNameRP} "),
                new FieldContent("degree", string.Empty),
                new FieldContent("title", string.Empty),
                new FieldContent("reviewer",
                    $"{vkr.ReviewerUP.SecondNameIP} {vkr.ReviewerUP.FirstNameIP} {vkr.ReviewerUP.MiddleNameIP}"),
                new FieldContent("year", vkr.Year.ToString())
            ); 
            
            return content;
        }
    }
}