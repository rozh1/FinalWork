using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Added_UploadableDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadableDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    OriginalName = table.Column<string>(nullable: true),
                    Length = table.Column<long>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    RejectReason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadableDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadableDocuments_UploadableDocuments_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "UploadableDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadableDocuments_UpdatedBy",
                table: "UploadableDocuments",
                column: "UpdatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadableDocuments");
        }
    }
}
