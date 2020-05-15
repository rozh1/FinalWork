using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Add_LocalizedDocumentStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocalizedDocumentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    LocalizedName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedDocumentStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadableDocuments_Status",
                table: "UploadableDocuments",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_UploadableDocuments_LocalizedDocumentStatuses_Status",
                table: "UploadableDocuments",
                column: "Status",
                principalTable: "LocalizedDocumentStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocalizedDocumentStatuses");

            migrationBuilder.DropIndex(
                name: "IX_UploadableDocuments_Status",
                table: "UploadableDocuments");
        }
    }
}
