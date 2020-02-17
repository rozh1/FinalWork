using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Add_Degree_to_VKR_and_add_ReviewerProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LecturerProfiles_AcademicDegrees_AcademicDegreeId",
                table: "LecturerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_LecturerProfiles_AcademicTitles_AcademicTitleId",
                table: "LecturerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_VKRs_UserProfiles_ReviewerUPId",
                table: "VKRs");

            migrationBuilder.AddColumn<Guid>(
                name: "DegreeId",
                table: "VKRs",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademicTitleId",
                table: "LecturerProfiles",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademicDegreeId",
                table: "LecturerProfiles",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "JobPlace",
                table: "LecturerProfiles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobPost",
                table: "LecturerProfiles",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ReviewerProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstNameIP = table.Column<string>(nullable: false),
                    SecondNameIP = table.Column<string>(nullable: false),
                    MiddleNameIP = table.Column<string>(nullable: false),
                    JobPlace = table.Column<string>(nullable: false),
                    JobPost = table.Column<string>(nullable: false),
                    AcademicTitleId = table.Column<Guid>(nullable: true),
                    AcademicDegreeId = table.Column<Guid>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewerProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewerProfile_AcademicDegrees_AcademicDegreeId",
                        column: x => x.AcademicDegreeId,
                        principalTable: "AcademicDegrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewerProfile_AcademicTitles_AcademicTitleId",
                        column: x => x.AcademicTitleId,
                        principalTable: "AcademicTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VKRs_DegreeId",
                table: "VKRs",
                column: "DegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewerProfile_AcademicDegreeId",
                table: "ReviewerProfile",
                column: "AcademicDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewerProfile_AcademicTitleId",
                table: "ReviewerProfile",
                column: "AcademicTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerProfiles_AcademicDegrees_AcademicDegreeId",
                table: "LecturerProfiles",
                column: "AcademicDegreeId",
                principalTable: "AcademicDegrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerProfiles_AcademicTitles_AcademicTitleId",
                table: "LecturerProfiles",
                column: "AcademicTitleId",
                principalTable: "AcademicTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VKRs_Degrees_DegreeId",
                table: "VKRs",
                column: "DegreeId",
                principalTable: "Degrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VKRs_ReviewerProfile_ReviewerUPId",
                table: "VKRs",
                column: "ReviewerUPId",
                principalTable: "ReviewerProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LecturerProfiles_AcademicDegrees_AcademicDegreeId",
                table: "LecturerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_LecturerProfiles_AcademicTitles_AcademicTitleId",
                table: "LecturerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_VKRs_Degrees_DegreeId",
                table: "VKRs");

            migrationBuilder.DropForeignKey(
                name: "FK_VKRs_ReviewerProfile_ReviewerUPId",
                table: "VKRs");

            migrationBuilder.DropTable(
                name: "ReviewerProfile");

            migrationBuilder.DropIndex(
                name: "IX_VKRs_DegreeId",
                table: "VKRs");

            migrationBuilder.DropColumn(
                name: "DegreeId",
                table: "VKRs");

            migrationBuilder.DropColumn(
                name: "JobPlace",
                table: "LecturerProfiles");

            migrationBuilder.DropColumn(
                name: "JobPost",
                table: "LecturerProfiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademicTitleId",
                table: "LecturerProfiles",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademicDegreeId",
                table: "LecturerProfiles",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerProfiles_AcademicDegrees_AcademicDegreeId",
                table: "LecturerProfiles",
                column: "AcademicDegreeId",
                principalTable: "AcademicDegrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerProfiles_AcademicTitles_AcademicTitleId",
                table: "LecturerProfiles",
                column: "AcademicTitleId",
                principalTable: "AcademicTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VKRs_UserProfiles_ReviewerUPId",
                table: "VKRs",
                column: "ReviewerUPId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
