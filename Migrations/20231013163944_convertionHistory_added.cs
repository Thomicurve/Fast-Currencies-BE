using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fast_currencies_be.Migrations
{
    /// <inheritdoc />
    public partial class convertionHistory_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LastRequestMonth",
                table: "Requests",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ConvertionHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CurrencyFromId = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrencyToId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConvertionHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConvertionHistory_Currencies_CurrencyFromId",
                        column: x => x.CurrencyFromId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConvertionHistory_Currencies_CurrencyToId",
                        column: x => x.CurrencyToId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConvertionHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConvertionHistory_CurrencyFromId",
                table: "ConvertionHistory",
                column: "CurrencyFromId");

            migrationBuilder.CreateIndex(
                name: "IX_ConvertionHistory_CurrencyToId",
                table: "ConvertionHistory",
                column: "CurrencyToId");

            migrationBuilder.CreateIndex(
                name: "IX_ConvertionHistory_UserId",
                table: "ConvertionHistory",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConvertionHistory");

            migrationBuilder.DropColumn(
                name: "LastRequestMonth",
                table: "Requests");
        }
    }
}
