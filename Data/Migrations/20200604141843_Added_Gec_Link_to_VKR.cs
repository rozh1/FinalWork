using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Added_Gec_Link_to_VKR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GecId",
                table: "VKRs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VKRs_GecId",
                table: "VKRs",
                column: "GecId");

            migrationBuilder.AddForeignKey(
                name: "FK_VKRs_GECs_GecId",
                table: "VKRs",
                column: "GecId",
                principalTable: "GECs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VKRs_GECs_GecId",
                table: "VKRs");

            migrationBuilder.DropIndex(
                name: "IX_VKRs_GecId",
                table: "VKRs");

            migrationBuilder.DropColumn(
                name: "GecId",
                table: "VKRs");
        }
    }
}
