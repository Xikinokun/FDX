using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FDXTestApp.Infrastructure.Migrations
{
    public partial class InitFifth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryStatus",
                table: "Recipient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Recipient",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                table: "Recipient");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Recipient");
        }
    }
}
