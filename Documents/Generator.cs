using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemplateEngine.Docx;

namespace FinalWork_BD_Test.Documents
{
    public static class Generator
    {
        private static ApplicationDbContext _context;
        public static FileResult Generate(string templateName, ApplicationDbContext context, User user)
        {
            _context = context;
            MethodInfo methodInfo = typeof(Generator).GetMethod("Generate" + templateName, BindingFlags.NonPublic | BindingFlags.Static);
            FileResult fileResult = (FileResult) methodInfo?.Invoke(null, new object?[] { user });
            return fileResult;
        }

        private static FileResult GenerateTest(User user)
        {
            Console.WriteLine("Invoked GenerateTest " + user.UserName);

            //TODO make check if file exist so return it instead generation

            foreach (var collection in _context.Entry(user).Collections)
            {
                collection.Load();
            }

            var userProfile = user.UserProfiles.FirstOrDefault(up => up.UpdatedBy == null);
            var studentProfile = user.StudentProfiles.FirstOrDefault(sp => sp.UpdatedBy == null);
            _context.Degrees.Load();

            var vkr = _context.VKRs.FirstOrDefault(vkr =>
                vkr.UpdatedByObj == null &&
                vkr.StudentUPId == userProfile.Id);

            var content = new Content(
                new FieldContent("program", vkr?.Degree.Name),
                new FieldContent("direction", "ТЕСТ"),
                new FieldContent("student", $"{studentProfile?.SecondNameRP} {studentProfile?.FirstNameRP} {studentProfile?.MiddleNameRP}")
                );

            File.Copy("Documents/Templates/Test.docx", $"Documents/Results/Test_{userProfile.SecondNameIP}_{userProfile.Id}.docx", true);

            using (var outputDocument = new TemplateProcessor($"Documents/Results/Test_{userProfile.SecondNameIP}_{userProfile.Id}.docx")
                .SetRemoveContentControls(true))
            {
                outputDocument.FillContent(content);
                outputDocument.SaveChanges();
            }

            var fileResult = new PhysicalFileResult($"C:\\Users\\Titan\\source\\repos\\FinalWork\\Documents\\Results\\Test_{userProfile.SecondNameIP}_{userProfile.Id}.docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = "TestFilled.docx" // Имя файла при загрузке пользователем.
            };

            return fileResult;
        }
    }
}
