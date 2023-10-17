using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fast_currencies_be.Migrations
{
    /// <inheritdoc />
    public partial class currencyRate_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO CurrencyRates (Id, CurrencyOriginId, CurrencyDestinationId, Rate)
                VALUES (1, 1, 2, 0.0029),
                       (2, 2, 1, 350),
                       (3, 2, 3, 0.85),
                       (4, 3, 2, 1.17),
                       (5, 1, 3, 0.0027),
                       (6, 3, 1, 370);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
