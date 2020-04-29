using System.IO;
using System.Linq;
using FinalWork_BD_Test.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemplateEngine.Docx;

namespace FinalWork_BD_Test.Documents
{
    public static partial class Generator
    {
        private static Content GenerateTitlePageRussianContent(User user)
        {
            foreach (var collection in _context.Entry(user).Collections)
            {
                collection.Load();
            }

            var userProfile = user.UserProfiles.FirstOrDefault(up => up.UpdatedBy == null);
            _context.Degrees.Load();

            var vkr = _context.VKRs
                .Include(l => l.SupervisorLP)
                .Include(l => l.SupervisorLP.AcademicDegree)
                .Include(l => l.SupervisorLP.AcademicTitle)
                .Include(l => l.Topic)
                .Include(l => l.SupervisorUP)
                .FirstOrDefault(vkr =>
                    vkr.UpdatedByObj == null &&
                    vkr.StudentUPId == userProfile.Id);

            var content = new Content(
                new FieldContent("direction", "ТЕСТ"),
                new FieldContent("topic", vkr?.Topic.Title),
                new FieldContent("student", $" {userProfile.FirstNameIP} {userProfile.MiddleNameIP} " +
                                            $"{userProfile.SecondNameIP}"),
                new FieldContent("lecture", $"{vkr?.SupervisorLP?.AcademicDegree.Name} " +
                                            $"{vkr?.SupervisorLP?.AcademicTitle.Name} " +
                                            $"{vkr?.SupervisorUP.FirstNameIP} " +
                                            $"{vkr?.SupervisorUP.MiddleNameIP} " +
                                            $"{vkr?.SupervisorUP.SecondNameIP}"),
                new FieldContent("year", vkr?.Year.ToString())
            );

            _fileName = $"TitlePageRussian_{userProfile.SecondNameIP}_{userProfile.Id}.docx";

            return content;
        }
    }
}


