using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data;
using FinalWork_BD_Test.Data.ConfigModels;
using FinalWork_BD_Test.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TemplateEngine.Docx;

namespace FinalWork_BD_Test.Documents
{
    public static class Generator
    {
        private static ApplicationDbContext _context;
        private static IOptions<DocumentsConfig> _documentsConfig;
        public static FileResult Generate(string templateName, ApplicationDbContext context, User user, IOptions<DocumentsConfig> documentsConfig)
        {
            _context = context;
            _documentsConfig = documentsConfig;

            MethodInfo methodInfo = typeof(Generator).GetMethod("Generate" + templateName, BindingFlags.NonPublic | BindingFlags.Static);
            FileResult fileResult = (FileResult) methodInfo?.Invoke(null, new object?[] { user });

            return fileResult;
        }

        private static FileResult GenerateTest(User user)
        {
            foreach (var collection in _context.Entry(user).Collections)
            {
                collection.Load();
            }

            var userProfile = user.UserProfiles.FirstOrDefault(up => up.UpdatedBy == null);

            string templateName = "Test";
            string fileName = $"{templateName}_{userProfile.SecondNameIP}_{userProfile.Id}.docx";
            string templatePath = $"{_documentsConfig.Value.TemplatesPath}\\{templateName}.docx";
            string resultsPath = $"{_documentsConfig.Value.ResultsPath}\\{fileName}";

            if (!File.Exists(resultsPath))
            {
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
                
                File.Copy(templatePath, resultsPath, true);

                using (var outputDocument = new TemplateProcessor(resultsPath)
                    .SetRemoveContentControls(true))
                {
                    outputDocument.FillContent(content);
                    outputDocument.SaveChanges();
                }
            }

            var fileResult = new PhysicalFileResult(resultsPath, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = "TestFilled.docx" // Имя файла при загрузке пользователем.
            };

            return fileResult;
        }

        private static FileResult GenerateTitlePageRussian(User user)
        {
            foreach (var collection in _context.Entry(user).Collections)
            {
                collection.Load();
            }

            var userProfile = user.UserProfiles.FirstOrDefault(up => up.UpdatedBy == null);

            const string templateName = "TitlePageRussian";
            var fileName = $"{templateName}_{userProfile.SecondNameIP}_{userProfile.Id}.docx";
            var templatePath = $"{_documentsConfig.Value.TemplatesPath}\\{templateName}.docx";
            var resultsPath = $"{_documentsConfig.Value.ResultsPath}\\{fileName}";

            if (!File.Exists(resultsPath))
            {
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
                    new FieldContent("lecture", $"{vkr?.SupervisorLP.AcademicDegree.Name} " +
                                                $"{vkr?.SupervisorLP.AcademicTitle.Name} " +
                                                $"{vkr?.SupervisorUP.FirstNameIP} " +
                                                $"{vkr?.SupervisorUP.MiddleNameIP} " +
                                                $"{vkr?.SupervisorUP.SecondNameIP}"),
                    new FieldContent("year", vkr.Year.ToString())
                );
                
                File.Copy(templatePath, resultsPath, true);

                using (var outputDocument = new TemplateProcessor(resultsPath)
                    .SetRemoveContentControls(true))
                {
                    outputDocument.FillContent(content);
                    outputDocument.SaveChanges();
                }
            }

            var fileResult = new PhysicalFileResult(resultsPath, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = "TitlePage.docx"
            };

            return fileResult;
        }

        private static FileResult GenerateTitlePageEnglish(User user)
        {
            foreach (var collection in _context.Entry(user).Collections)
            {
                collection.Load();
            }

            var userProfile = user.UserProfiles.FirstOrDefault(up => up.UpdatedBy == null);

            const string templateName = "TitlePageEnglish";
            var fileName = $"{templateName}_{userProfile.SecondNameIP}_{userProfile.Id}.docx";
            var templatePath = $"{_documentsConfig.Value.TemplatesPath}\\{templateName}.docx";
            var resultsPath = $"{_documentsConfig.Value.ResultsPath}\\{fileName}";

            var fileResult = new PhysicalFileResult(resultsPath, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = "TitlePageEnglish.docx"
            };

            return fileResult;
        }

        private static FileResult GenerateTask(User user)
        {
            foreach (var collection in _context.Entry(user).Collections)
            {
                collection.Load();
            }

            var userProfile = user.UserProfiles.FirstOrDefault(up => up.UpdatedBy == null);

            const string templateName = "Task";
            var fileName = $"{templateName}_{userProfile.SecondNameIP}_{userProfile.Id}.docx";
            var templatePath = $"{_documentsConfig.Value.TemplatesPath}\\{templateName}.docx";
            var resultsPath = $"{_documentsConfig.Value.ResultsPath}\\{fileName}";

            if (!File.Exists(resultsPath))
            {
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
                    new FieldContent("supervisor", $"{vkr?.SupervisorLP.AcademicDegree.Name} " +
                                                $"{vkr?.SupervisorLP.AcademicTitle.Name} " +
                                                $"{vkr?.SupervisorUP.FirstNameIP} " +
                                                $"{vkr?.SupervisorUP.MiddleNameIP} " +
                                                $"{vkr?.SupervisorUP.SecondNameIP}"),
                    new FieldContent("year", vkr.Year.ToString())
                );
                
                File.Copy(templatePath, resultsPath, true);

                using (var outputDocument = new TemplateProcessor(resultsPath)
                    .SetRemoveContentControls(true))
                {
                    outputDocument.FillContent(content);
                    outputDocument.SaveChanges();
                }
            }

            var fileResult = new PhysicalFileResult(resultsPath, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = "Task.docx"
            };

            return fileResult;
        }
    }
}
