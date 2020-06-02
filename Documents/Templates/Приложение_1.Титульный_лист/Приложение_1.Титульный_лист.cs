using System;
using System.Linq;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.EntityFrameworkCore;
using TemplateEngine.Docx;

namespace FinalWork_BD_Test.Documents
{
    public static partial class Generator
    {
        private static Content GenerateTitlePageContent(User user)
        {
            var vkr = _context.VKRs
                .Include(x => x.Topic)
                .Include(x => x.StudentUP)
                .Include(x => x.SupervisorUP)
                .Include(x => x.SupervisorLP.AcademicDegree)
                .Include(x => x.SupervisorLP.AcademicTitle)
                .FirstOrDefault(x => x.StudentUP.User.Id == user.Id && x.UpdatedByObj == null);

            if (vkr == null) 
                return null;

            var studentFio =
                $"{vkr.StudentUP.FirstNameIP[0]}.{vkr.StudentUP.MiddleNameIP[0]}. {vkr.StudentUP.SecondNameIP}";
            var supervisorFio =
                $"{vkr.SupervisorUP.FirstNameIP[0]}.{vkr.SupervisorUP.MiddleNameIP[0]}. {vkr.SupervisorUP.SecondNameIP}";
            
            var valuesToFill = new Content(
                new FieldContent("topic", vkr.Topic.Title),
                new FieldContent("year", vkr.Year.ToString()),
                new FieldContent("student", studentFio),
                new FieldContent("degree", vkr.SupervisorLP.AcademicDegree.Name.ToLower()),
                new FieldContent("title", vkr.SupervisorLP.AcademicTitle.Name.ToLower()),
                new FieldContent("supervisor", supervisorFio)
            );

            return valuesToFill;
        }
    }
}