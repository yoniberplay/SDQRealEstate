using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDQRealEstate.Infrastructure.Identity.Migrations
{
    /// <inheritdoc />
    public partial class Refactorizaciondeentiedad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foto",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                schema: "Identity",
                table: "Users");
        }
    }
}
