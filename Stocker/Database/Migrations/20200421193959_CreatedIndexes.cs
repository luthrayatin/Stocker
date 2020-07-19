using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocker.Database.Migrations
{
    public partial class CreatedIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "StockExchangeId",
                "Stocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                "IX_Stocks_StockExchangeId",
                "Stocks",
                "StockExchangeId");

            migrationBuilder.CreateIndex(
                "IX_StockExchanges_Name",
                "StockExchanges",
                "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                "FK_Stocks_StockExchanges_StockExchangeId",
                "Stocks",
                "StockExchangeId",
                "StockExchanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Stocks_StockExchanges_StockExchangeId",
                "Stocks");

            migrationBuilder.DropIndex(
                "IX_Stocks_StockExchangeId",
                "Stocks");

            migrationBuilder.DropIndex(
                "IX_StockExchanges_Name",
                "StockExchanges");

            migrationBuilder.DropColumn(
                "StockExchangeId",
                "Stocks");
        }
    }
}