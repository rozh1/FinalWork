using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinalWork_BD_Test.Data.Localization.Enum;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Data;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.AspNetCore.Identity;

namespace FinalWork_BD_Test.Data.Migrations
{
    public static class DataSeed
    {
        private static bool _changed = false;
        public static void Seed(DbContext сontext, UserManager<User> userManager)
        {
            var appContext = сontext as ApplicationDbContext;
            if (appContext == null)
                return;

            // Степени высшего образования
            if (!appContext.Degrees.Any())
            {
                appContext.Degrees.AddRange(
                    new Degree() { Name = "Бакалавр", Id = Guid.Parse("d16c749c-16f3-48b8-afc8-305fdde6bba5") },
                    new Degree() { Name = "Специалист", Id = Guid.Parse("536c21a8-5b51-44e2-9d18-1d43818f4497") },
                    new Degree() { Name = "Магистр", Id = Guid.Parse("c1497517-87d1-489b-8ac5-af2ba99ea532") }
                    );
                _changed = true;
            }

            // Полы
            if (!appContext.Genders.Any())
            {
                appContext.Genders.AddRange(
                    new Gender() { Name = "Мужской", Id = Guid.Parse("cba1747d-f0cc-446e-82db-3966af086d49") },
                    new Gender() { Name = "Женский", Id = Guid.Parse("334d77d3-84f9-480c-a76e-edfa0d93e5ff") }
                    );
                _changed = true;
            }

            // Формы обучения
            if (!appContext.EducationForms.Any())
            {
                appContext.EducationForms.AddRange(
                    new EducationForm() { Name = "Дневная", Id = Guid.Parse("ff3b4b4d-c6f1-4625-93c4-300630a020fc") },
                    new EducationForm() { Name = "Вечерняя", Id = Guid.Parse("f177a0e5-db4a-4881-a37f-dd499dbdce02") },
                    new EducationForm() { Name = "Ускоренная", Id = Guid.Parse("49fe1a83-7658-4d75-be0c-05e9a76b9fa0") }
                    );
                _changed = true;
            }

            // Семестры
            if (!appContext.Semesters.Any())
            {
                appContext.Semesters.AddRange(
                    new Semester() { Name = "Осень", Id = Guid.Parse("cc412346-074a-40c6-83a0-2377df518d51") },
                    new Semester() { Name = "Весна", Id = Guid.Parse("3b96611e-f0f7-4564-9695-05c97a141ce6") }
                    );
                _changed = true;
            }

            // Учёные степени
            if (!appContext.AcademicDegrees.Any())
            {
                appContext.AcademicDegrees.AddRange(
                    new AcademicDegree() { Name = "к.т.н.", Id = Guid.Parse("d552a078-ad35-4275-a969-b0d09681e018") },
                    new AcademicDegree() { Name = "к.ф.-м.н.", Id = Guid.Parse("69a4f8c8-f3bb-4d8a-a064-0be5f8040e44") },
                    new AcademicDegree() { Name = "д.т.н.", Id = Guid.Parse("ecc78e5f-9723-4794-ba08-2156bd20f86d") },
                    new AcademicDegree() { Name = "д.ф.-м.н.", Id = Guid.Parse("3c1a4473-04ee-4a45-b47e-99fc2580b886") }
                    );
                _changed = true;
            }

            // Ученые звания
            if (!appContext.AcademicTitles.Any())
            {
                appContext.AcademicTitles.AddRange(
                    new AcademicTitle() { Name = "Доцент", Id = Guid.Parse("922b0692-e54a-49c2-b89f-413b49124f57") },
                    new AcademicTitle() { Name = "Профессор", Id = Guid.Parse("1e576607-6fd7-42e4-a474-61702647a672") }
                    );
                _changed = true;
            }

            if (!appContext.Roles.Any())
            {
                appContext.Roles.AddRange(
                    new Role { Name = "Admin", NormalizedName = "ADMIN", Id = Guid.Parse("c9b59c8c-76a6-4483-ab61-f592302e1352") },
                    new Role { Name = "Supervisor", NormalizedName = "SUPERVISOR", Id = Guid.Parse("0f0ebe12-f363-4daf-b1d3-42165f4b2874") },
                    new Role { Name = "Student", NormalizedName = "STUDENT", Id = Guid.Parse("dc8bfb02-7fa0-4c2a-be0d-69fcf7dacb10") }
                    );

                _changed = true;
            }

            if (appContext.UserRoles.FirstOrDefault(ur => ur.RoleId == appContext.Roles.FirstOrDefault(r => r.Name == "Admin").Id) == null)
            {
                User adminUser = new User
                {
                    Id = Guid.Parse("51bba30f-0b3a-4cde-86dd-17419ce1b130"),
                    UserName = "admin@asu.vkr",
                };

                string pass = GeneratePassword();
                File.WriteAllText("AdminPass.txt", pass);
                userManager.CreateAsync(adminUser, pass).Wait();
                userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                
                adminUser.UserProfiles = new List<UserProfile>(){ new UserProfile(){CreatedDate = DateTime.Now, FirstNameIP = "Админ", SecondNameIP = "Админ"} };
                _changed = true;
            }

            // Локализация DocumentStatus
            if (!appContext.LocalizedDocumentStatuses.Any())
            {
                appContext.LocalizedDocumentStatuses.AddRange(
                    new LocalizedDocumentStatus() {Id = DocumentStatus.Verification, LocalizedName = "На проверке"},
                    new LocalizedDocumentStatus() {Id = DocumentStatus.Approved, LocalizedName = "Утвержден"},
                    new LocalizedDocumentStatus() {Id = DocumentStatus.Rejected, LocalizedName = "Отклонен"}
                );
                _changed = true;
            }

            if (_changed)
                appContext.SaveChanges();
        }

        public static string GeneratePassword(
            int requiredLength = 12,
            int requiredUniqueChars = 4,
            bool requireDigit = true,
            bool requireLowercase = true,
            bool requireNonAlphanumeric = true,
            bool requireUppercase = true)
        {
            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "!@$?_-"                        // non-alphanumeric
            };
            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (requireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);

            if (requireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            if (requireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[2][rand.Next(0, randomChars[2].Length)]);

            if (requireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[3][rand.Next(0, randomChars[3].Length)]);

            for (int i = chars.Count; i < requiredLength
                                      || chars.Distinct().Count() < requiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }

    }
}
