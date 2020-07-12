using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocker.Database.Migrations
{
    public partial class CreatedIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StockExchangeId",
                table: "Stocks",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_StockExchangeId",
                table: "Stocks",
                column: "StockExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_StockExchanges_Name",
                table: "StockExchanges",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_StockExchanges_StockExchangeId",
                table: "Stocks",
                column: "StockExchangeId",
                principalTable: "StockExchanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_StockExchanges_StockExchangeId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_StockExchangeId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_StockExchanges_Name",
                table: "StockExchanges");

            migrationBuilder.DropColumn(
                name: "StockExchangeId",
                table: "Stocks");
        }
    }
}
