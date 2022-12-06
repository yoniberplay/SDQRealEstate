using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDQRealEstate.Infrastucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RelaciondeMejora : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MejorasId",
                table: "Propiedad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_MejorasId",
                table: "Propiedad",
                column: "MejorasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedad_Mejora_MejorasId",
                table: "Propiedad",
                column: "MejorasId",
                principalTable: "Mejora",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propiedad_Mejora_MejorasId",
                table: "Propiedad");

            migrationBuilder.DropIndex(
                name: "IX_Propiedad_MejorasId",
                table: "Propiedad");

            migrationBuilder.DropColumn(
                name: "MejorasId",
                table: "Propiedad");
        }
    }
}
