using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fast_currencies_be.Migrations
{
    /// <inheritdoc />
    public partial class initdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Subscriptions (Id, Description, MaxRequests)
                VALUES (1, 'Free', 10),
                       (2, 'Trial', 100),
                       (3, 'Pro', 0);

                INSERT INTO Currencies (Id, Symbol, Leyend, IC)
                VALUES (1, 'ARS', 'Pesos argentinos', 0.002),
                       (2, 'USD', 'Dólares americanos', 1),
                       (3, 'EUR', 'Euros', 1.09),
                       (4, 'KC', 'Coronas checas', 0.043);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
