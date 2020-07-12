using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Stocker.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Symbol = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Ticker = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TradingPlatforms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingPlatforms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockExchanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockExchanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockExchanges_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockTransactions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StockId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    PricePerUnitCurrencyId = table.Column<int>(nullable: true),
                    PricePerUnitValueMinor = table.Column<long>(nullable: true),
                    StockExchangeId = table.Column<int>(nullable: false),
                    TradingPlatformId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    UserCurrencyId = table.Column<int>(nullable: false),
                    ConversionRate = table.Column<decimal>(nullable: false),
                    LoggedAt = table.Column<DateTimeOffset>(nullable: false),
                    CommissionCurrencyId = table.Column<int>(nullable: true),
                    CommissionValueMinor = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockTransactions_StockExchanges_StockExchangeId",
                        column: x => x.StockExchangeId,
                        principalTable: "StockExchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransactions_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransactions_TradingPlatforms_TradingPlatformId",
                        column: x => x.TradingPlatformId,
                        principalTable: "TradingPlatforms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransactions_Currencies_UserCurrencyId",
                        column: x => x.UserCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransactions_Currencies_CommissionCurrencyId",
                        column: x => x.CommissionCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockTransactions_Currencies_PricePerUnitCurrencyId",
                        column: x => x.PricePerUnitCurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockExchanges_CurrencyId",
                table: "StockExchanges",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_StockExchangeId",
                table: "StockTransactions",
                column: "StockExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_StockId",
                table: "StockTransactions",
                column: "StockId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_TradingPlatformId",
                table: "StockTransactions",
                column: "TradingPlatformId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_UserCurrencyId",
                table: "StockTransactions",
                column: "UserCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_UserId",
                table: "StockTransactions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_CommissionCurrencyId",
                table: "StockTransactions",
                column: "CommissionCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_PricePerUnitCurrencyId",
                table: "StockTransactions",
                column: "PricePerUnitCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockTransactions");

            migrationBuilder.DropTable(
                name: "StockExchanges");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "TradingPlatforms");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Currencies");
        }
    }
}
