using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fast_currencies_be.Migrations
{
    /// <inheritdoc />
    public partial class currencyRate_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrencyRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CurrencyOriginId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencyDestinationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Rate = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyRates_Currencies_CurrencyDestinationId",
                        column: x => x.CurrencyDestinationId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurrencyRates_Currencies_CurrencyOriginId",
                        column: x => x.CurrencyOriginId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_CurrencyDestinationId",
                table: "CurrencyRates",
                column: "CurrencyDestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRates_CurrencyOriginId",
                table: "CurrencyRates",
                column: "CurrencyOriginId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyRates");
        }
    }
}
