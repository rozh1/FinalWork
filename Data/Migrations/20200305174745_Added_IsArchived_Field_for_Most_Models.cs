using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Added_IsArchived_Field_for_Most_Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "UserProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Topics",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ReviewerProfiles",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "ReviewerProfiles",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "LecturerProfiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ReviewerProfiles_UpdatedBy",
                table: "ReviewerProfiles",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewerProfiles_ReviewerProfiles_UpdatedBy",
                table: "ReviewerProfiles",
                column: "UpdatedBy",
                principalTable: "ReviewerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewerProfiles_ReviewerProfiles_UpdatedBy",
                table: "ReviewerProfiles");

            migrationBuilder.DropIndex(
                name: "IX_ReviewerProfiles_UpdatedBy",
                table: "ReviewerProfiles");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "UserProfiles");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ReviewerProfiles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ReviewerProfiles");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "LecturerProfiles");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "AspNetUsers");
        }
    }
}
