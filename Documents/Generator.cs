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
        
        public static FileResult Generate(string templateName, ApplicationDbContext context, User user, IOptions<DocumentsConfig> documentsConfig)
        {
            _context = context;
            _documentsConfig = documentsConfig;

            // ToDo make more structured Results directory
            var templatePath = $"{_documentsConfig.Value.TemplatesPath}\\{templateName}\\{templateName}.docx";
            var resultsPath = $"{_documentsConfig.Value.ResultsPath}\\{templateName}\\{_fileName}";

            Directory.CreateDirectory($"{_documentsConfig.Value.ResultsPath}\\{templateName}\\");

            MethodInfo methodInfo = typeof(Generator).GetMethod($"Generate{templateName}Content", BindingFlags.NonPublic | BindingFlags.Static);
            Content content = (Content)methodInfo?.Invoke(null, new object?[] { user });

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
