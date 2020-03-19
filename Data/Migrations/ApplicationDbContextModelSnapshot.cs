﻿// <auto-generated />
using System;
using FinalWork_BD_Test.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FinalWork_BD_Test.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Data.AcademicDegree", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AcademicDegrees");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Data.AcademicTitle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("AcademicTitles");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Data.EducationForm", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(10)")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("EducationForms");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Data.Gender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(7)")
                        .HasMaxLength(7);

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Data.Semester", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(5)")
                        .HasMaxLength(5);

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Degree", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.GEC", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChiefId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("DeputyId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EducationFormId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ChiefId");

                    b.HasIndex("DeputyId");

                    b.HasIndex("EducationFormId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("GECs");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Intermidate.GecMemberIntermediate", b =>
                {
                    b.Property<Guid>("GecId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MemberProfileId")
                        .HasColumnType("uuid");

                    b.HasKey("GecId", "MemberProfileId");

                    b.HasIndex("MemberProfileId");

                    b.ToTable("GecMemberIntermediates");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Profiles.GecMemberProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AcademicDegreeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AcademicTitleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstNameIP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<string>("JobPlace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("JobPost")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleNameIP")
                        .HasColumnType("text");

                    b.Property<string>("SecondNameIP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AcademicDegreeId");

                    b.HasIndex("AcademicTitleId");

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UserId");

                    b.ToTable("GecMemberProfiles");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Profiles.LecturerProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AcademicDegreeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AcademicTitleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstNameDP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstNameRP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<string>("JobPlace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("JobPost")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleNameDP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleNameRP")
                        .HasColumnType("text");

                    b.Property<string>("SecondNameDP")
                        .HasColumnType("text");

                    b.Property<string>("SecondNameRP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AcademicDegreeId");

                    b.HasIndex("AcademicTitleId");

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UserId");

                    b.ToTable("LecturerProfiles");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Profiles.ReviewerProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AcademicDegreeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AcademicTitleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstNameIP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<string>("JobPlace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("JobPost")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleNameIP")
                        .HasColumnType("text");

                    b.Property<string>("SecondNameIP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AcademicDegreeId");

                    b.HasIndex("AcademicTitleId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("ReviewerProfiles");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Profiles.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FirstNameIP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<string>("MiddleNameIP")
                        .HasColumnType("text");

                    b.Property<string>("SecondNameIP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UserId");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.StudentProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("EducationFormId")
                        .HasColumnType("uuid");

                    b.Property<string>("FirstNameDP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstNameRP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("GenderId")
                        .HasColumnType("uuid");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("character varying(5)")
                        .HasMaxLength(5);

                    b.Property<string>("MiddleNameDP")
                        .HasColumnType("text");

                    b.Property<string>("MiddleNameRP")
                        .HasColumnType("text");

                    b.Property<string>("SecondNameDP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SecondNameRP")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("EducationFormId");

                    b.HasIndex("GenderId");

                    b.HasIndex("UpdatedBy");

                    b.HasIndex("UserId");

                    b.ToTable("StudentProfiles");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.VKR", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("DegreeId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsArchived")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("ReviewerUPId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SemesterId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("StudentSPId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("StudentUPId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SupervisorLPId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SupervisorUPId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("TopicId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UpdatedBy")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Year")
                        .HasColumnType("numeric")
                        .HasMaxLength(4);

                    b.HasKey("Id");

                    b.HasIndex("DegreeId");

                    b.HasIndex("ReviewerUPId");

                    b.HasIndex("SemesterId");

                    b.HasIndex("StudentSPId");

                    b.HasIndex("StudentUPId");

                    b.HasIndex("SupervisorLPId");

                    b.HasIndex("SupervisorUPId");

                    b.HasIndex("TopicId");

                    b.HasIndex("UpdatedBy");

                    b.ToTable("VKRs");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.GEC", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.Profiles.GecMemberProfile", "Chief")
                        .WithMany()
                        .HasForeignKey("ChiefId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWork_BD_Test.Data.Models.Profiles.GecMemberProfile", "Deputy")
                        .WithMany()
                        .HasForeignKey("DeputyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWork_BD_Test.Data.Models.Data.EducationForm", "EducationForm")
                        .WithMany()
                        .HasForeignKey("EducationFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWork_BD_Test.Data.Models.GEC", "UpdatedByObj")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Intermidate.GecMemberIntermediate", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.GEC", "GEC")
                        .WithMany("Members")
                        .HasForeignKey("GecId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWork_BD_Test.Data.Models.Profiles.GecMemberProfile", "MemberProfile")
                        .WithMany("GECs")
                        .HasForeignKey("MemberProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Profiles.GecMemberProfile", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.Data.AcademicDegree", "AcademicDegree")
                        .WithMany()
                        .HasForeignKey("AcademicDegreeId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.Data.AcademicTitle", "AcademicTitle")
                        .WithMany()
                        .HasForeignKey("AcademicTitleId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.Profiles.GecMemberProfile", "UpdatedByObj")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.HasOne("FinalWork_BD_Test.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Profiles.LecturerProfile", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.Data.AcademicDegree", "AcademicDegree")
                        .WithMany()
                        .HasForeignKey("AcademicDegreeId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.Data.AcademicTitle", "AcademicTitle")
                        .WithMany()
                        .HasForeignKey("AcademicTitleId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.Profiles.LecturerProfile", "UpdatedByObj")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.HasOne("FinalWork_BD_Test.Data.Models.User", "User")
                        .WithMany("LecturerProfiles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Profiles.ReviewerProfile", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.Data.AcademicDegree", "AcademicDegree")
                        .WithMany()
                        .HasForeignKey("AcademicDegreeId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.Data.AcademicTitle", "AcademicTitle")
                        .WithMany()
                        .HasForeignKey("AcademicTitleId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.Profiles.ReviewerProfile", "UpdatedByObj")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Profiles.UserProfile", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.Profiles.UserProfile", "UpdatedByObj")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.HasOne("FinalWork_BD_Test.Data.Models.User", "User")
                        .WithMany("UserProfiles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.StudentProfile", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.Data.EducationForm", "EducationForm")
                        .WithMany()
                        .HasForeignKey("EducationFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWork_BD_Test.Data.Models.Data.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWork_BD_Test.Data.Models.StudentProfile", "UpdatedByObj")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");

                    b.HasOne("FinalWork_BD_Test.Data.Models.User", "User")
                        .WithMany("StudentProfiles")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.Topic", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWork_BD_Test.Data.Models.Topic", "UpdatedByObj")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("FinalWork_BD_Test.Data.Models.VKR", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.Degree", "Degree")
                        .WithMany()
                        .HasForeignKey("DegreeId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.Profiles.ReviewerProfile", "ReviewerUP")
                        .WithMany()
                        .HasForeignKey("ReviewerUPId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.Data.Semester", "Semester")
                        .WithMany()
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWork_BD_Test.Data.Models.StudentProfile", "StudentSP")
                        .WithMany()
                        .HasForeignKey("StudentSPId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.Profiles.UserProfile", "StudentUP")
                        .WithMany()
                        .HasForeignKey("StudentUPId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.Profiles.LecturerProfile", "SupervisorLP")
                        .WithMany()
                        .HasForeignKey("SupervisorLPId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.Profiles.UserProfile", "SupervisorUP")
                        .WithMany()
                        .HasForeignKey("SupervisorUPId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.Topic", "Topic")
                        .WithMany()
                        .HasForeignKey("TopicId");

                    b.HasOne("FinalWork_BD_Test.Data.Models.VKR", "UpdatedByObj")
                        .WithMany()
                        .HasForeignKey("UpdatedBy");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinalWork_BD_Test.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("FinalWork_BD_Test.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
