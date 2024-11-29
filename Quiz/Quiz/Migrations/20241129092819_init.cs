using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Quiz.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    CardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    HolderName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Balance = table.Column<float>(type: "real", nullable: false),
                    IsActice = table.Column<bool>(type: "bit", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.CardNumber);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceCardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    DestinationCardNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isSuccessful = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transactions_Cards_DestinationCardNumber",
                        column: x => x.DestinationCardNumber,
                        principalTable: "Cards",
                        principalColumn: "CardNumber");
                    table.ForeignKey(
                        name: "FK_Transactions_Cards_SourceCardNumber",
                        column: x => x.SourceCardNumber,
                        principalTable: "Cards",
                        principalColumn: "CardNumber");
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "CardNumber", "Balance", "HolderName", "IsActice", "Password" },
                values: new object[,]
                {
                    { "5859831000619801", 2000f, "Armin", true, "123" },
                    { "5859831000619802", 2000f, "Mehdi", true, "123" },
                    { "5859831000619803", 2000f, "Ali", true, "123" },
                    { "5859831000619804", 2000f, "Arash", true, "123" },
                    { "5859831000619805", 2000f, "Maryam", true, "123" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DestinationCardNumber",
                table: "Transactions",
                column: "DestinationCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SourceCardNumber",
                table: "Transactions",
                column: "SourceCardNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Cards");
        }
    }
}
