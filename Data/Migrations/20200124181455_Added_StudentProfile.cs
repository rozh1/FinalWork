using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Added_StudentProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Degrees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Degrees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationForms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 7, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Semesters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semesters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    FirstNameRP = table.Column<string>(nullable: true),
                    SecondNameRP = table.Column<string>(nullable: true),
                    MiddleNameRP = table.Column<string>(nullable: true),
                    FirstNameDP = table.Column<string>(nullable: true),
                    SecondNameDP = table.Column<string>(nullable: true),
                    MiddleNameDP = table.Column<string>(nullable: true),
                    DegreeId = table.Column<Guid>(nullable: true),
                    GenderId = table.Column<Guid>(nullable: true),
                    EducationFormId = table.Column<Guid>(nullable: true),
                    Group = table.Column<string>(maxLength: 5, nullable: true),
                    GraduateYear = table.Column<int>(nullable: false),
                    GraduateSemesterId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_Degrees_DegreeId",
                        column: x => x.DegreeId,
                        principalTable: "Degrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_EducationForms_EducationFormId",
                        column: x => x.EducationFormId,
                        principalTable: "EducationForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_Semesters_GraduateSemesterId",
                        column: x => x.GraduateSemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_StudentProfiles_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_DegreeId",
                table: "StudentProfiles",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_EducationFormId",
                table: "StudentProfiles",
                column: "EducationFormId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_GenderId",
                table: "StudentProfiles",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_GraduateSemesterId",
                table: "StudentProfiles",
                column: "GraduateSemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_UpdatedBy",
                table: "StudentProfiles",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_UserId",
                table: "StudentProfiles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentProfiles");

            migrationBuilder.DropTable(
                name: "Degrees");

            migrationBuilder.DropTable(
                name: "EducationForms");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Semesters");
        }
    }
}
