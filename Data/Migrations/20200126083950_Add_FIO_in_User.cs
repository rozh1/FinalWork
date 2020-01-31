using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalWork_BD_Test.Migrations
{
    public partial class Add_FIO_in_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstNameIP",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleNameIP",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondNameIP",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstNameIP",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MiddleNameIP",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecondNameIP",
                table: "AspNetUsers");
        }
    }
}
