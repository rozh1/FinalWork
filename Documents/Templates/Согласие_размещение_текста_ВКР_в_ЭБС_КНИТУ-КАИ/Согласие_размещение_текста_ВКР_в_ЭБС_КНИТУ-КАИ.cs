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
        private static Content GenerateAgreementPostTextContent(User user)
        {
            _context.Entry(user).Collection(x => x.UserProfiles).Load();
            var userUP = user.UserProfiles.FirstOrDefault(x => x.UpdatedByObj == null);
            
            _context.Entry(user).Collection(x => x.StudentProfiles).Load();
            var userSP = user.StudentProfiles.FirstOrDefault(x => x.UpdatedByObj == null);

            if (userUP == null && userSP == null)
                return null;

            var vkr = _context.VKRs
                .Include(l => l.Topic)
                .FirstOrDefault(x => x.UpdatedByObj == null && x.StudentUPId == userUP.Id);

            if (vkr == null)
                return null;

            var content = new Content(
                new FieldContent("topic", vkr.Topic.Title),
                new FieldContent("studentRP", $"{userSP.SecondNameRP} {userSP.FirstNameRP} {userSP.MiddleNameRP} "),
                new FieldContent("studentIP", $"{userUP.SecondNameIP} {userUP.FirstNameIP} {userUP.MiddleNameIP} "),
                new FieldContent("course", userSP.Group[0].ToString())
            ); 
            
            return content;
        }
    }
}