using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDQRealEstate.Infrastucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddcolumnCodigotopropiedadtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Codigo",
                table: "Propiedad",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Propiedad");
        }
    }
}
