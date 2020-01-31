using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Topic4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Topics_UpdatedByObjId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_UpdatedByObjId",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "UpdatedByObjId",
                table: "Topics");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Topics",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Topics",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_Topics_AuthorId",
                table: "Topics",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_UpdatedBy",
                table: "Topics",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_AspNetUsers_AuthorId",
                table: "Topics",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Topics_Topics_UpdatedBy",
                table: "Topics",
                column: "UpdatedBy",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_AspNetUsers_AuthorId",
                table: "Topics");

            migrationBuilder.DropForeignKey(
                name: "FK_Topics_Topics_UpdatedBy",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_AuthorId",
                table: "Topics");

            migrationBuilder.DropIndex(
                name: "IX_Topics_UpdatedBy",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Topics");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Topics",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 256);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedByObjId",
                table: "Topics",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string));

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
    }
}
