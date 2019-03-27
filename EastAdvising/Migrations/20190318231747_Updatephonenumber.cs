using Microsoft.EntityFrameworkCore.Migrations;

namespace EastAdvising.Migrations
{
    public partial class Updatephonenumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Advisor",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PhoneNumber",
                table: "Advisor",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
