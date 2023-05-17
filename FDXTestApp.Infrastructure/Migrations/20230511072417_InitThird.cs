using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FDXTestApp.Infrastructure.Migrations
{
    public partial class InitThird : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Recipient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Recipient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
