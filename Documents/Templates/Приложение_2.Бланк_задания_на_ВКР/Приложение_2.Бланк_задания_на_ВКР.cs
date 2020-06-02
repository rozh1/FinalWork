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
            _context.Entry(user).Collection(x => x.UserProfiles).Load();
            var userUP = user.UserProfiles.FirstOrDefault(x => x.UpdatedByObj == null);
            
            _context.Entry(user).Collection(x => x.StudentProfiles).Load();
            var userSP = user.StudentProfiles.FirstOrDefault(x => x.UpdatedByObj == null);

            if (userUP == null && userSP == null)
                return null;

            var vkr = _context.VKRs
                .Include(l => l.Topic)
                .Include(l => l.SupervisorUP)
                .FirstOrDefault(x => x.UpdatedByObj == null && x.StudentUPId == userUP.Id);

            if (vkr == null)
                return null;

            var content = new Content(
                new FieldContent("topic", vkr.Topic.Title),
                new FieldContent("student", $"{userSP.SecondNameRP} " +
                                            $"{userSP.FirstNameRP} " +
                                            $"{userSP.MiddleNameRP} "),
                new FieldContent("supervisor",
                    $"{vkr.SupervisorUP.FirstNameIP[0]}.{vkr.SupervisorUP.MiddleNameIP[0]}. {vkr.SupervisorUP.SecondNameIP}"),
                new FieldContent("year", vkr.Year.ToString())
            ); 
            
            return content;
        }
    }
}
