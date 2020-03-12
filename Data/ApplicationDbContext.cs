using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FinalWork_BD_Test.Data.Models;
using FinalWork_BD_Test.Data.Models.Data;
using FinalWork_BD_Test.Data.Models.Intermidate;
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
        public DbSet<ReviewerProfile> ReviewerProfiles { get; set; }
        public DbSet<GecMemberProfile> GecMemberProfiles { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<EducationForm> EducationForms { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<AcademicDegree> AcademicDegrees { get; set; }
        public DbSet<AcademicTitle> AcademicTitles { get; set; }
        public DbSet<VKR> VKRs { get; set; }
        public DbSet<GEC> GECs { get; set; }
        public DbSet<GecMemberIntermediate> GecMemberIntermediates { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<GecMemberIntermediate>()
                .HasKey(x => new { x.GecId, x.MemberProfileId});
            builder.Entity<GecMemberIntermediate>()
                .HasOne(x => x.GEC)
                .WithMany(g => g.Members)
                .HasForeignKey(x => x.GecId);
            builder.Entity<GecMemberIntermediate>()
                .HasOne(x => x.MemberProfile)
                .WithMany(m => m.GECs)
                .HasForeignKey(x => x.MemberProfileId);
        }
    }
}
