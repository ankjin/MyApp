using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAppShared.Migrations
{
    public partial class modRateToDecimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "RateReturn",
                table: "InvestmentModels",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "InvestAmount",
                table: "InvestmentModels",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                column: "CreatedDate",
                value: new DateTime(2022, 2, 6, 22, 10, 7, 227, DateTimeKind.Local).AddTicks(4118));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RateReturn",
                table: "InvestmentModels",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "InvestAmount",
                table: "InvestmentModels",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b74ddd14-6340-4840-95c2-db12554843e5"),
                column: "CreatedDate",
                value: new DateTime(2022, 2, 3, 12, 38, 3, 865, DateTimeKind.Local).AddTicks(5448));
        }
    }
}
