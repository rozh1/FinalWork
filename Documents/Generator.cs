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
    public static partial class Generator
    {
        private static ApplicationDbContext _context;
        private static IOptions<DocumentsConfig> _documentsConfig;
        private static string _fileName;
        
        private static Dictionary<string, string> _contentNames = new Dictionary<string, string>()
        {
            { "Приложение_1.Титульный_лист", "TitlePage"},
            { "Приложение_2.Бланк_задания_на_ВКР", "Task"},
            { "Приложение_3.Бланк_акта_предварительной_защиты_ВКР", "PreliminaryProtection"},
            { "Приложение_4.Бланк_отзыва_руководителя_на_ВКР", "HeadReview"},
            { "Приложение_5.Бланк_рецензии_на_ВКР", "Review"},
            { "Согласие_размещение_текста_ВКР_в_ЭБС_КНИТУ-КАИ", "AgreementPostText"}
        };
        public static FileResult Generate(string templateName, ApplicationDbContext context, User user, IOptions<DocumentsConfig> documentsConfig)
        {
            _context = context;
            _documentsConfig = documentsConfig;

            
            var userProfile = _context
                .Users
                .Include(c => c.UserProfiles)
                .FirstOrDefault(c => c.Id == user.Id)?
                .UserProfiles
                .FirstOrDefault(p => p.UpdatedByObj == null);

            _fileName = $"{templateName}_{userProfile.SecondNameIP}_{userProfile.Id}.docx";
            
            var templatePath = $"{_documentsConfig.Value.TemplatesPath}{Path.DirectorySeparatorChar}{templateName}{Path.DirectorySeparatorChar}{templateName}.docx";
            var resultsPath = $"{_documentsConfig.Value.ResultsPath}{Path.DirectorySeparatorChar}{templateName}{Path.DirectorySeparatorChar}{_fileName}";

            Directory.CreateDirectory($"{_documentsConfig.Value.ResultsPath}{Path.DirectorySeparatorChar}{templateName}{Path.DirectorySeparatorChar}");

            MethodInfo methodInfo = typeof(Generator).GetMethod($"Generate{_contentNames[templateName]}Content", BindingFlags.NonPublic | BindingFlags.Static);
            Content content = (Content)methodInfo?.Invoke(null, new object[] { user });

            File.Copy(templatePath, resultsPath, true);

            if (content != null)
            {
                using (var outputDocument = new TemplateProcessor(resultsPath)
                    .SetRemoveContentControls(true))
                {
                    outputDocument.FillContent(content);
                    outputDocument.SaveChanges();
                }
            }
            var fileResult = new PhysicalFileResult(resultsPath, "application/vnd.openxmlformats-officedocument.wordprocessingml.document")
            {
                FileDownloadName = $"{templateName}.docx"
            };

            return fileResult;
        }
    }
}
