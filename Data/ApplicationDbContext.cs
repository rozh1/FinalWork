using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Data;
using FinalWork_BD_Test.Data.Models.Profiles;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalWork_BD_Test.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        public DbSet<LecturerProfile> LecturerProfiles { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<EducationForm> EducationForms { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }
        public DbSet<AcademicTitle> AcademicTitles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
