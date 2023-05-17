using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FDXTestApp.Infrastructure.Migrations
{
    public partial class InitSecond : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipient_Sms_SmsId",
                table: "Recipient");

            migrationBuilder.AlterColumn<Guid>(
                name: "SmsId",
                table: "Recipient",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipient_Sms_SmsId",
                table: "Recipient",
                column: "SmsId",
                principalTable: "Sms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipient_Sms_SmsId",
                table: "Recipient");

            migrationBuilder.AlterColumn<Guid>(
                name: "SmsId",
                table: "Recipient",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipient_Sms_SmsId",
                table: "Recipient",
                column: "SmsId",
                principalTable: "Sms",
                principalColumn: "Id");
        }
    }
}
