using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Added_VKR_Field_in_UploadableDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "VkrId",
                table: "UploadableDocuments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UploadableDocuments_VkrId",
                table: "UploadableDocuments",
                column: "VkrId");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadableDocuments_VKRs_VkrId",
                table: "UploadableDocuments",
                column: "VkrId",
                principalTable: "VKRs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UploadableDocuments_VKRs_VkrId",
                table: "UploadableDocuments");

            migrationBuilder.DropIndex(
                name: "IX_UploadableDocuments_VkrId",
                table: "UploadableDocuments");

            migrationBuilder.DropColumn(
                name: "VkrId",
                table: "UploadableDocuments");
        }
    }
}
