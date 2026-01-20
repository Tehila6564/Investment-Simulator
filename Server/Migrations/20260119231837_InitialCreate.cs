using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvestmentOptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    RequiredAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    ExpectedReturn = table.Column<decimal>(type: "TEXT", nullable: false),
                    DurationSeconds = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvestmentOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "ActiveInvestments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    InvestedAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    ExpectedReturn = table.Column<decimal>(type: "TEXT", nullable: false),
                    EndsAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveInvestments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveInvestments_Users_Username",
                        column: x => x.Username,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "InvestmentOptions",
                columns: new[] { "Id", "DurationSeconds", "ExpectedReturn", "Name", "RequiredAmount" },
                values: new object[,]
                {
                    { "long", 60, 3000m, "Long-term investment", 1000m },
                    { "mid", 30, 250m, "Mid-term investment", 100m },
                    { "short", 10, 20m, "Short-term investment", 10m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveInvestments_Username",
                table: "ActiveInvestments",
                column: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveInvestments");

            migrationBuilder.DropTable(
                name: "InvestmentOptions");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
