using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Moved_FIO_to_UserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstNameIP",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MiddleNameIP",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecondNameIP",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    FirstNameIP = table.Column<string>(nullable: true),
                    SecondNameIP = table.Column<string>(nullable: true),
                    MiddleNameIP = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_UserProfiles_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UpdatedBy",
                table: "UserProfiles",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.AddColumn<string>(
                name: "FirstNameIP",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleNameIP",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondNameIP",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }
    }
}
