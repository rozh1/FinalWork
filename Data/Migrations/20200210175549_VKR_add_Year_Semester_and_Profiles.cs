using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class VKR_add_Year_Semester_and_Profiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReviewerUPId",
                table: "VKRs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SemesterId",
                table: "VKRs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentSPId",
                table: "VKRs",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SupervisorLPId",
                table: "VKRs",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Year",
                table: "VKRs",
                maxLength: 4,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_VKRs_ReviewerUPId",
                table: "VKRs",
                column: "ReviewerUPId");

            migrationBuilder.CreateIndex(
                name: "IX_VKRs_SemesterId",
                table: "VKRs",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_VKRs_StudentSPId",
                table: "VKRs",
                column: "StudentSPId");

            migrationBuilder.CreateIndex(
                name: "IX_VKRs_SupervisorLPId",
                table: "VKRs",
                column: "SupervisorLPId");

            migrationBuilder.AddForeignKey(
                name: "FK_VKRs_UserProfiles_ReviewerUPId",
                table: "VKRs",
                column: "ReviewerUPId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VKRs_Semesters_SemesterId",
                table: "VKRs",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VKRs_StudentProfiles_StudentSPId",
                table: "VKRs",
                column: "StudentSPId",
                principalTable: "StudentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VKRs_LecturerProfiles_SupervisorLPId",
                table: "VKRs",
                column: "SupervisorLPId",
                principalTable: "LecturerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VKRs_UserProfiles_ReviewerUPId",
                table: "VKRs");

            migrationBuilder.DropForeignKey(
                name: "FK_VKRs_Semesters_SemesterId",
                table: "VKRs");

            migrationBuilder.DropForeignKey(
                name: "FK_VKRs_StudentProfiles_StudentSPId",
                table: "VKRs");

            migrationBuilder.DropForeignKey(
                name: "FK_VKRs_LecturerProfiles_SupervisorLPId",
                table: "VKRs");

            migrationBuilder.DropIndex(
                name: "IX_VKRs_ReviewerUPId",
                table: "VKRs");

            migrationBuilder.DropIndex(
                name: "IX_VKRs_SemesterId",
                table: "VKRs");

            migrationBuilder.DropIndex(
                name: "IX_VKRs_StudentSPId",
                table: "VKRs");

            migrationBuilder.DropIndex(
                name: "IX_VKRs_SupervisorLPId",
                table: "VKRs");

            migrationBuilder.DropColumn(
                name: "ReviewerUPId",
                table: "VKRs");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "VKRs");

            migrationBuilder.DropColumn(
                name: "StudentSPId",
                table: "VKRs");

            migrationBuilder.DropColumn(
                name: "SupervisorLPId",
                table: "VKRs");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "VKRs");
        }
    }
}
