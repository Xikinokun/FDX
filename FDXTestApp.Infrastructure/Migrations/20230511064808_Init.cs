using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FDXTestApp.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    From = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipient",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipient_Sms_SmsId",
                        column: x => x.SmsId,
                        principalTable: "Sms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recipient_SmsId",
                table: "Recipient",
                column: "SmsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipient");

            migrationBuilder.DropTable(
                name: "Sms");
        }
    }
}
