using System.Linq;
using FinalWork_BD_Test.Data.Models;
using Microsoft.EntityFrameworkCore;
using TemplateEngine.Docx;

namespace FinalWork_BD_Test.Documents
{
    public static partial class Generator
    {
        private static Content GenerateTaskContent(User user)
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

            var studentProf = user.StudentProfiles.FirstOrDefault(up => up.UpdatedBy == null);

            var content = new Content(
                new FieldContent("topic", vkr?.Topic.Title),
                new FieldContent("student", $"{studentProf.SecondNameRP} " +
                                            $"{studentProf.FirstNameRP} " +
                                            $"{studentProf.MiddleNameRP} "
                                            ),
                new FieldContent("supervisor", $"{vkr?.SupervisorLP?.AcademicDegree.Name} " +
                                            $"{vkr?.SupervisorLP?.AcademicTitle.Name} " +
                                            $"{vkr?.SupervisorUP.FirstNameIP} " +
                                            $"{vkr?.SupervisorUP.MiddleNameIP} " +
                                            $"{vkr?.SupervisorUP.SecondNameIP}"),
                new FieldContent("year", vkr?.Year.ToString())
            ); 
            
            
            _fileName = $"Task_{userProfile.SecondNameIP}_{userProfile.Id}.docx";

            return content;
        }
    }
}
