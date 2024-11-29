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
                    Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Daylitransaction = table.Column<float>(type: "real", nullable: false),
                    SetedLimitationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertingPasswordWrongly = table.Column<int>(type: "int", nullable: false)
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
                columns: new[] { "CardNumber", "Balance", "Daylitransaction", "HolderName", "InsertingPasswordWrongly", "IsActice", "Password", "SetedLimitationDate" },
                values: new object[,]
                {
                    { "5859831000619801", 2000f, 0f, "Armin", 0, true, "123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "5859831000619802", 2000f, 0f, "Mehdi", 0, true, "123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "5859831000619803", 2000f, 0f, "Ali", 0, true, "123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "5859831000619804", 2000f, 0f, "Arash", 0, true, "123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "5859831000619805", 2000f, 0f, "Maryam", 0, true, "123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
