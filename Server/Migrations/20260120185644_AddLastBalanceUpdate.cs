using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class AddLastBalanceUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvestmentOptions",
                keyColumn: "Id",
                keyValue: "long");

            migrationBuilder.DeleteData(
                table: "InvestmentOptions",
                keyColumn: "Id",
                keyValue: "mid");

            migrationBuilder.DeleteData(
                table: "InvestmentOptions",
                keyColumn: "Id",
                keyValue: "short");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastBalanceUpdate",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastBalanceUpdate",
                table: "Users");

            migrationBuilder.InsertData(
                table: "InvestmentOptions",
                columns: new[] { "Id", "DurationSeconds", "ExpectedReturn", "Name", "RequiredAmount" },
                values: new object[,]
                {
                    { "long", 60, 3000m, "Long-term investment", 1000m },
                    { "mid", 30, 250m, "Mid-term investment", 100m },
                    { "short", 10, 20m, "Short-term investment", 10m }
                });
        }
    }
}
