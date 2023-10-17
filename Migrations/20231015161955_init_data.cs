using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fast_currencies_be.Migrations
{
    /// <inheritdoc />
    public partial class init_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Subscriptions (Id, Description, MaxRequests)
                VALUES (1, 'Free', 5),
                       (2, 'Premium', 15),
                       (3, 'Enterprise', 30);

                INSERT INTO Currencies (Id, Code)
                VALUES (1, 'ARS'),
                       (2, 'USD'),
                       (3, 'EUR');
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
