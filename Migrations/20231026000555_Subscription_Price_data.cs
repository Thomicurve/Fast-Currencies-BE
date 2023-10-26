using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fast_currencies_be.Migrations
{
    /// <inheritdoc />
    public partial class Subscription_Price_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                UPDATE Subscriptions SET Price = 0 WHERE Id = 1;
                UPDATE Subscriptions SET Price = 60 WHERE Id = 2;
                UPDATE Subscriptions SET Price = 150 WHERE Id = 3;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
