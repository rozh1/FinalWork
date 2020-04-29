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
        private static Content GenerateTitlePageEnglishContent(User user)
        {

            foreach (var collection in _context.Entry(user).Collections)
            {
                collection.Load();
            }

            var userProfile = user.UserProfiles.FirstOrDefault(up => up.UpdatedBy == null);

            _fileName = $"TitlePageEnglish_{userProfile.SecondNameIP}_{userProfile.Id}.docx";
            return null;
        }
    }
}


