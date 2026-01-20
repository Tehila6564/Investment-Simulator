using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedInvestmentOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "InvestmentOptions",
                columns: new[] { "Id", "DurationSeconds", "ExpectedReturn", "Name", "RequiredAmount" },
                values: new object[,]
                {
                    { "1", 30, 525m, "Short-term Bond", 500m },
                    { "2", 60, 1680m, "Tech Stock Bundle", 1500m },
                    { "3", 120, 6250m, "Venture Capital", 5000m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvestmentOptions",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "InvestmentOptions",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "InvestmentOptions",
                keyColumn: "Id",
                keyValue: "3");
        }
    }
}
