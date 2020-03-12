using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Added_IsArchived_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GECs_GecMemberProfiles_ChiefId",
                table: "GECs");

            migrationBuilder.DropForeignKey(
                name: "FK_GECs_GecMemberProfiles_DeputyId",
                table: "GECs");

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "VKRs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<Guid>(
                name: "DeputyId",
                table: "GECs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ChiefId",
                table: "GECs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GECs_GecMemberProfiles_ChiefId",
                table: "GECs",
                column: "ChiefId",
                principalTable: "GecMemberProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GECs_GecMemberProfiles_DeputyId",
                table: "GECs",
                column: "DeputyId",
                principalTable: "GecMemberProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GECs_GecMemberProfiles_ChiefId",
                table: "GECs");

            migrationBuilder.DropForeignKey(
                name: "FK_GECs_GecMemberProfiles_DeputyId",
                table: "GECs");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "VKRs");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeputyId",
                table: "GECs",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "ChiefId",
                table: "GECs",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_GECs_GecMemberProfiles_ChiefId",
                table: "GECs",
                column: "ChiefId",
                principalTable: "GecMemberProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GECs_GecMemberProfiles_DeputyId",
                table: "GECs",
                column: "DeputyId",
                principalTable: "GecMemberProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
