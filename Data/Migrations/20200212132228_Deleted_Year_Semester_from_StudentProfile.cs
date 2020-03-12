using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Deleted_Year_Semester_from_StudentProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentProfiles_Semesters_GraduateSemesterId",
                table: "StudentProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_VKRs_Semesters_SemesterId",
                table: "VKRs");

            migrationBuilder.DropIndex(
                name: "IX_StudentProfiles_GraduateSemesterId",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "GraduateSemesterId",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "GraduateYear",
                table: "StudentProfiles");

            migrationBuilder.AlterColumn<Guid>(
                name: "SemesterId",
                table: "VKRs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VKRs_Semesters_SemesterId",
                table: "VKRs",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VKRs_Semesters_SemesterId",
                table: "VKRs");

            migrationBuilder.AlterColumn<Guid>(
                name: "SemesterId",
                table: "VKRs",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "GraduateSemesterId",
                table: "StudentProfiles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "GraduateYear",
                table: "StudentProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentProfiles_GraduateSemesterId",
                table: "StudentProfiles",
                column: "GraduateSemesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentProfiles_Semesters_GraduateSemesterId",
                table: "StudentProfiles",
                column: "GraduateSemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VKRs_Semesters_SemesterId",
                table: "VKRs",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
