using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace WebAppShared.Migrations
{
    public partial class modInvestmentPayout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PayoutDate",
                table: "InvestmentPayouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                column: "CreatedDate",
                value: new DateTime(2022, 2, 2, 13, 2, 47, 916, DateTimeKind.Local).AddTicks(8859));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PayoutDate",
                table: "InvestmentPayouts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                column: "CreatedDate",
                value: new DateTime(2022, 1, 30, 10, 19, 53, 826, DateTimeKind.Local).AddTicks(7208));
        }
    }
}
