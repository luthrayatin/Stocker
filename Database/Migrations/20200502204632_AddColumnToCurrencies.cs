using Microsoft.EntityFrameworkCore.Migrations;

namespace Stocker.Database.Migrations
{
    public partial class AddColumnToCurrencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MinorToMajorMultiplier",
                table: "Currencies",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinorToMajorMultiplier",
                table: "Currencies");
        }
    }
}
