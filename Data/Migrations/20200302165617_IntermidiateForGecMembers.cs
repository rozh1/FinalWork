using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class IntermidiateForGecMembers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GecMemberIntermediate_GECs_GecId",
                table: "GecMemberIntermediate");

            migrationBuilder.DropForeignKey(
                name: "FK_GecMemberIntermediate_GecMemberProfiles_MemberProfileId",
                table: "GecMemberIntermediate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GecMemberIntermediate",
                table: "GecMemberIntermediate");

            migrationBuilder.RenameTable(
                name: "GecMemberIntermediate",
                newName: "GecMemberIntermediates");

            migrationBuilder.RenameIndex(
                name: "IX_GecMemberIntermediate_MemberProfileId",
                table: "GecMemberIntermediates",
                newName: "IX_GecMemberIntermediates_MemberProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GecMemberIntermediates",
                table: "GecMemberIntermediates",
                columns: new[] { "GecId", "MemberProfileId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GecMemberIntermediates_GECs_GecId",
                table: "GecMemberIntermediates",
                column: "GecId",
                principalTable: "GECs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GecMemberIntermediates_GecMemberProfiles_MemberProfileId",
                table: "GecMemberIntermediates",
                column: "MemberProfileId",
                principalTable: "GecMemberProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GecMemberIntermediates_GECs_GecId",
                table: "GecMemberIntermediates");

            migrationBuilder.DropForeignKey(
                name: "FK_GecMemberIntermediates_GecMemberProfiles_MemberProfileId",
                table: "GecMemberIntermediates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GecMemberIntermediates",
                table: "GecMemberIntermediates");

            migrationBuilder.RenameTable(
                name: "GecMemberIntermediates",
                newName: "GecMemberIntermediate");

            migrationBuilder.RenameIndex(
                name: "IX_GecMemberIntermediates_MemberProfileId",
                table: "GecMemberIntermediate",
                newName: "IX_GecMemberIntermediate_MemberProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GecMemberIntermediate",
                table: "GecMemberIntermediate",
                columns: new[] { "GecId", "MemberProfileId" });

            migrationBuilder.AddForeignKey(
                name: "FK_GecMemberIntermediate_GECs_GecId",
                table: "GecMemberIntermediate",
                column: "GecId",
                principalTable: "GECs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GecMemberIntermediate_GecMemberProfiles_MemberProfileId",
                table: "GecMemberIntermediate",
                column: "MemberProfileId",
                principalTable: "GecMemberProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
