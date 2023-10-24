using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fast_currencies_be.Migrations
{
    /// <inheritdoc />
    public partial class PriceHistory_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ConvertedPrice",
                table: "ConvertionHistory",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceToConvert",
                table: "ConvertionHistory",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConvertedPrice",
                table: "ConvertionHistory");

            migrationBuilder.DropColumn(
                name: "PriceToConvert",
                table: "ConvertionHistory");
        }
    }
}
