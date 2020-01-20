using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Topic3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Topics",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "Topics",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByObjId",
                table: "Topics",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Topics_UpdatedByObjId",
                table: "Topics",
                column: "UpdatedByObjId");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Topics_UpdatedByObjId",
                table: "Topics",
                column: "UpdatedByObjId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Topics_UpdatedByObjId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_UpdatedByObjId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "UpdatedByObjId",
                table: "Topics");
        }
    }
}
