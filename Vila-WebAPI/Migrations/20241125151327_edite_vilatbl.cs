using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vila_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class edite_vilatbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SellPrice",
                table: "vilas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "dayPrice",
                table: "vilas",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellPrice",
                table: "vilas");

            migrationBuilder.DropColumn(
                name: "dayPrice",
                table: "vilas");
        }
    }
}
