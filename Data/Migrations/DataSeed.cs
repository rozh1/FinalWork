using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Data;

namespace FinalWork_BD_Test.Data.Migrations
{
    public static class DataSeed
    {
        private static bool _changed = false;
        public static void Seed(DbContext Context)
        {
            var appContext = Context as ApplicationDbContext;
            if (appContext == null)
                return;

            if (!appContext.Degrees.Any())
            {
                appContext.Degrees.AddRange(
                    new Degree() { Name = "Бакалавр" },
                    new Degree() { Name = "Специалист" },
                    new Degree() { Name = "Магистр" }
                    );
                _changed = true;
            }

            if (!appContext.Genders.Any())
            {
                appContext.Genders.AddRange(
                    new Gender() { Name = "Мужской" },
                    new Gender() { Name = "Женский" }
                    );
                _changed = true;
            }

            if (!appContext.EducationForms.Any())
            {
                appContext.EducationForms.AddRange(
                    new EducationForm() { Name = "Дневная" },
                    new EducationForm() { Name = "Вечерняя" },
                    new EducationForm() { Name = "Ускоренная" }
                    );
                _changed = true;
            }

            if (!appContext.Semesters.Any())
            {
                appContext.Semesters.AddRange(
                    new Semester() { Name = "Осень"},
                    new Semester() { Name = "Весна"}
                    );
                _changed = true;
            }

            if (!appContext.AcademicDegrees.Any())
            {
                appContext.AcademicDegrees.AddRange(
                    new AcademicDegree(){Name="к.т.н."},
                    new AcademicDegree(){Name="к.ф.-м.н."},
                    new AcademicDegree(){Name="д.т.н."},
                    new AcademicDegree(){Name="д.ф.-м.н."}
                    );
                _changed = true;
            }

            if (!appContext.AcademicTitles.Any())
            {
                appContext.AcademicTitles.AddRange(
                    new AcademicTitle() { Name="Доцент"},
                    new AcademicTitle() { Name="Профессор"}
                    );
                _changed = true;
            }

            if (_changed)
                appContext.SaveChanges();

        }
    }
}
