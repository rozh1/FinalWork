using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class VKR_add_Topic_and_Supervisor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LecturerProfiles_AcademicDegrees_AcademicDegreeId",
                table: "LecturerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_LecturerProfiles_AcademicTitles_AcademicTitleId",
                table: "LecturerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_AspNetUsers_SuperVisorId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_SuperVisorId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "SuperVisorId",
                table: "Topics");

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademicTitleId",
                table: "LecturerProfiles",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademicDegreeId",
                table: "LecturerProfiles",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "VKRs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    StudentUPId = table.Column<Guid>(nullable: true),
                    TopicId = table.Column<Guid>(nullable: true),
                    SupervisorUPId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VKRs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VKRs_UserProfiles_StudentUPId",
                        column: x => x.StudentUPId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VKRs_UserProfiles_SupervisorUPId",
                        column: x => x.SupervisorUPId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VKRs_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VKRs_VKRs_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "VKRs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VKRs_StudentUPId",
                table: "VKRs",
                column: "StudentUPId");

            migrationBuilder.CreateIndex(
                name: "IX_VKRs_SupervisorUPId",
                table: "VKRs",
                column: "SupervisorUPId");

            migrationBuilder.CreateIndex(
                name: "IX_VKRs_TopicId",
                table: "VKRs",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_VKRs_UpdatedBy",
                table: "VKRs",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerProfiles_AcademicDegrees_AcademicDegreeId",
                table: "LecturerProfiles",
                column: "AcademicDegreeId",
                principalTable: "AcademicDegrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerProfiles_AcademicTitles_AcademicTitleId",
                table: "LecturerProfiles",
                column: "AcademicTitleId",
                principalTable: "AcademicTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LecturerProfiles_AcademicDegrees_AcademicDegreeId",
                table: "LecturerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_LecturerProfiles_AcademicTitles_AcademicTitleId",
                table: "LecturerProfiles");

            migrationBuilder.DropTable(
                name: "VKRs");

            migrationBuilder.AddColumn<Guid>(
                name: "SuperVisorId",
                table: "Topics",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademicTitleId",
                table: "LecturerProfiles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "AcademicDegreeId",
                table: "LecturerProfiles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Topics_SuperVisorId",
                table: "Topics",
                column: "SuperVisorId");

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerProfiles_AcademicDegrees_AcademicDegreeId",
                table: "LecturerProfiles",
                column: "AcademicDegreeId",
                principalTable: "AcademicDegrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LecturerProfiles_AcademicTitles_AcademicTitleId",
                table: "LecturerProfiles",
                column: "AcademicTitleId",
                principalTable: "AcademicTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_AspNetUsers_SuperVisorId",
                table: "Topics",
                column: "SuperVisorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
