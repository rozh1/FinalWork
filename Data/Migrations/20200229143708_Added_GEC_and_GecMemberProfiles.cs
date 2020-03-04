using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Added_GEC_and_GecMemberProfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewerProfile_AcademicDegrees_AcademicDegreeId",
                table: "ReviewerProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewerProfile_AcademicTitles_AcademicTitleId",
                table: "ReviewerProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_VKRs_ReviewerProfile_ReviewerUPId",
                table: "VKRs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewerProfile",
                table: "ReviewerProfile");

            migrationBuilder.RenameTable(
                name: "ReviewerProfile",
                newName: "ReviewerProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewerProfile_AcademicTitleId",
                table: "ReviewerProfiles",
                newName: "IX_ReviewerProfiles_AcademicTitleId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewerProfile_AcademicDegreeId",
                table: "ReviewerProfiles",
                newName: "IX_ReviewerProfiles_AcademicDegreeId");

            migrationBuilder.AlterColumn<string>(
                name: "SecondNameDP",
                table: "LecturerProfiles",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewerProfiles",
                table: "ReviewerProfiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GecMemberProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true),
                    FirstNameIP = table.Column<string>(nullable: false),
                    SecondNameIP = table.Column<string>(nullable: false),
                    MiddleNameIP = table.Column<string>(nullable: false),
                    JobPlace = table.Column<string>(nullable: false),
                    JobPost = table.Column<string>(nullable: false),
                    AcademicTitleId = table.Column<Guid>(nullable: true),
                    AcademicDegreeId = table.Column<Guid>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GecMemberProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GecMemberProfiles_AcademicDegrees_AcademicDegreeId",
                        column: x => x.AcademicDegreeId,
                        principalTable: "AcademicDegrees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GecMemberProfiles_AcademicTitles_AcademicTitleId",
                        column: x => x.AcademicTitleId,
                        principalTable: "AcademicTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GecMemberProfiles_GecMemberProfiles_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "GecMemberProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GecMemberProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GECs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<Guid>(nullable: true),
                    Specialty = table.Column<string>(nullable: true),
                    EducationFormId = table.Column<Guid>(nullable: false),
                    ChiefId = table.Column<Guid>(nullable: true),
                    DeputyId = table.Column<Guid>(nullable: true),
                    IsArchived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GECs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GECs_GecMemberProfiles_ChiefId",
                        column: x => x.ChiefId,
                        principalTable: "GecMemberProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GECs_GecMemberProfiles_DeputyId",
                        column: x => x.DeputyId,
                        principalTable: "GecMemberProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GECs_EducationForms_EducationFormId",
                        column: x => x.EducationFormId,
                        principalTable: "EducationForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GECs_GECs_UpdatedBy",
                        column: x => x.UpdatedBy,
                        principalTable: "GECs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GecMemberIntermediate",
                columns: table => new
                {
                    GecId = table.Column<Guid>(nullable: false),
                    MemberProfileId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GecMemberIntermediate", x => new { x.GecId, x.MemberProfileId });
                    table.ForeignKey(
                        name: "FK_GecMemberIntermediate_GECs_GecId",
                        column: x => x.GecId,
                        principalTable: "GECs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GecMemberIntermediate_GecMemberProfiles_MemberProfileId",
                        column: x => x.MemberProfileId,
                        principalTable: "GecMemberProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GecMemberIntermediate_MemberProfileId",
                table: "GecMemberIntermediate",
                column: "MemberProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GecMemberProfiles_AcademicDegreeId",
                table: "GecMemberProfiles",
                column: "AcademicDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_GecMemberProfiles_AcademicTitleId",
                table: "GecMemberProfiles",
                column: "AcademicTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_GecMemberProfiles_UpdatedBy",
                table: "GecMemberProfiles",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_GecMemberProfiles_UserId",
                table: "GecMemberProfiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GECs_ChiefId",
                table: "GECs",
                column: "ChiefId");

            migrationBuilder.CreateIndex(
                name: "IX_GECs_DeputyId",
                table: "GECs",
                column: "DeputyId");

            migrationBuilder.CreateIndex(
                name: "IX_GECs_EducationFormId",
                table: "GECs",
                column: "EducationFormId");

            migrationBuilder.CreateIndex(
                name: "IX_GECs_UpdatedBy",
                table: "GECs",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewerProfiles_AcademicDegrees_AcademicDegreeId",
                table: "ReviewerProfiles",
                column: "AcademicDegreeId",
                principalTable: "AcademicDegrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewerProfiles_AcademicTitles_AcademicTitleId",
                table: "ReviewerProfiles",
                column: "AcademicTitleId",
                principalTable: "AcademicTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VKRs_ReviewerProfiles_ReviewerUPId",
                table: "VKRs",
                column: "ReviewerUPId",
                principalTable: "ReviewerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewerProfiles_AcademicDegrees_AcademicDegreeId",
                table: "ReviewerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewerProfiles_AcademicTitles_AcademicTitleId",
                table: "ReviewerProfiles");

            migrationBuilder.DropForeignKey(
                name: "FK_VKRs_ReviewerProfiles_ReviewerUPId",
                table: "VKRs");

            migrationBuilder.DropTable(
                name: "GecMemberIntermediate");

            migrationBuilder.DropTable(
                name: "GECs");

            migrationBuilder.DropTable(
                name: "GecMemberProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewerProfiles",
                table: "ReviewerProfiles");

            migrationBuilder.RenameTable(
                name: "ReviewerProfiles",
                newName: "ReviewerProfile");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewerProfiles_AcademicTitleId",
                table: "ReviewerProfile",
                newName: "IX_ReviewerProfile_AcademicTitleId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewerProfiles_AcademicDegreeId",
                table: "ReviewerProfile",
                newName: "IX_ReviewerProfile_AcademicDegreeId");

            migrationBuilder.AlterColumn<string>(
                name: "SecondNameDP",
                table: "LecturerProfiles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewerProfile",
                table: "ReviewerProfile",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewerProfile_AcademicDegrees_AcademicDegreeId",
                table: "ReviewerProfile",
                column: "AcademicDegreeId",
                principalTable: "AcademicDegrees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewerProfile_AcademicTitles_AcademicTitleId",
                table: "ReviewerProfile",
                column: "AcademicTitleId",
                principalTable: "AcademicTitles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VKRs_ReviewerProfile_ReviewerUPId",
                table: "VKRs",
                column: "ReviewerUPId",
                principalTable: "ReviewerProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
