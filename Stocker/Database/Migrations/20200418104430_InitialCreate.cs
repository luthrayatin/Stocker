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
                "Currencies",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Symbol = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Currencies", x => x.Id); });

            migrationBuilder.CreateTable(
                "Stocks",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Ticker = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Stocks", x => x.Id); });

            migrationBuilder.CreateTable(
                "TradingPlatforms",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_TradingPlatforms", x => x.Id); });

            migrationBuilder.CreateTable(
                "Users",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

            migrationBuilder.CreateTable(
                "StockExchanges",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockExchanges", x => x.Id);
                    table.ForeignKey(
                        "FK_StockExchanges_Currencies_CurrencyId",
                        x => x.CurrencyId,
                        "Currencies",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "StockTransactions",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy",
                            NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
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
                        "FK_StockTransactions_StockExchanges_StockExchangeId",
                        x => x.StockExchangeId,
                        "StockExchanges",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_StockTransactions_Stocks_StockId",
                        x => x.StockId,
                        "Stocks",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_StockTransactions_TradingPlatforms_TradingPlatformId",
                        x => x.TradingPlatformId,
                        "TradingPlatforms",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_StockTransactions_Currencies_UserCurrencyId",
                        x => x.UserCurrencyId,
                        "Currencies",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_StockTransactions_Users_UserId",
                        x => x.UserId,
                        "Users",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_StockTransactions_Currencies_CommissionCurrencyId",
                        x => x.CommissionCurrencyId,
                        "Currencies",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_StockTransactions_Currencies_PricePerUnitCurrencyId",
                        x => x.PricePerUnitCurrencyId,
                        "Currencies",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_StockExchanges_CurrencyId",
                "StockExchanges",
                "CurrencyId");

            migrationBuilder.CreateIndex(
                "IX_StockTransactions_StockExchangeId",
                "StockTransactions",
                "StockExchangeId");

            migrationBuilder.CreateIndex(
                "IX_StockTransactions_StockId",
                "StockTransactions",
                "StockId");

            migrationBuilder.CreateIndex(
                "IX_StockTransactions_TradingPlatformId",
                "StockTransactions",
                "TradingPlatformId");

            migrationBuilder.CreateIndex(
                "IX_StockTransactions_UserCurrencyId",
                "StockTransactions",
                "UserCurrencyId");

            migrationBuilder.CreateIndex(
                "IX_StockTransactions_UserId",
                "StockTransactions",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_StockTransactions_CommissionCurrencyId",
                "StockTransactions",
                "CommissionCurrencyId");

            migrationBuilder.CreateIndex(
                "IX_StockTransactions_PricePerUnitCurrencyId",
                "StockTransactions",
                "PricePerUnitCurrencyId");

            migrationBuilder.CreateIndex(
                "IX_Users_Email",
                "Users",
                "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "StockTransactions");

            migrationBuilder.DropTable(
                "StockExchanges");

            migrationBuilder.DropTable(
                "Stocks");

            migrationBuilder.DropTable(
                "TradingPlatforms");

            migrationBuilder.DropTable(
                "Users");

            migrationBuilder.DropTable(
                "Currencies");
        }
    }
}