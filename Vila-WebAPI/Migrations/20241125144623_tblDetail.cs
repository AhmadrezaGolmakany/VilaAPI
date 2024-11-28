using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vila_WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class tblDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "details",
                columns: table => new
                {
                    DeyailaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VilaId = table.Column<int>(type: "int", nullable: false),
                    What = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_details", x => x.DeyailaId);
                    table.ForeignKey(
                        name: "FK_details_vilas_VilaId",
                        column: x => x.VilaId,
                        principalTable: "vilas",
                        principalColumn: "VilaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_details_VilaId",
                table: "details",
                column: "VilaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "details");
        }
    }
}
