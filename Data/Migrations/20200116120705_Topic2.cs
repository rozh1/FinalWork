using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Topic2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_AspNetUsers_SuperVisorId",
                table: "Topics");

            migrationBuilder.AlterColumn<Guid>(
                name: "SuperVisorId",
                table: "Topics",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_AspNetUsers_SuperVisorId",
                table: "Topics",
                column: "SuperVisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_AspNetUsers_SuperVisorId",
                table: "Topics");

            migrationBuilder.AlterColumn<Guid>(
                name: "SuperVisorId",
                table: "Topics",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_AspNetUsers_SuperVisorId",
                table: "Topics",
                column: "SuperVisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
