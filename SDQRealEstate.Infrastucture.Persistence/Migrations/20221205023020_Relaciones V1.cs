using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDQRealEstate.Infrastucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RelacionesV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoPropiedad",
                table: "Propiedad");

            migrationBuilder.DropColumn(
                name: "TipoVenta",
                table: "Propiedad");

            migrationBuilder.AddColumn<int>(
                name: "TipoPropiedadId",
                table: "Propiedad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoVentaId",
                table: "Propiedad",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_TipoPropiedadId",
                table: "Propiedad",
                column: "TipoPropiedadId");

            migrationBuilder.CreateIndex(
                name: "IX_Propiedad_TipoVentaId",
                table: "Propiedad",
                column: "TipoVentaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedad_TipoPropiedades_TipoPropiedadId",
                table: "Propiedad",
                column: "TipoPropiedadId",
                principalTable: "TipoPropiedades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Propiedad_TipoVenta_TipoVentaId",
                table: "Propiedad",
                column: "TipoVentaId",
                principalTable: "TipoVenta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Propiedad_TipoPropiedades_TipoPropiedadId",
                table: "Propiedad");

            migrationBuilder.DropForeignKey(
                name: "FK_Propiedad_TipoVenta_TipoVentaId",
                table: "Propiedad");

            migrationBuilder.DropIndex(
                name: "IX_Propiedad_TipoPropiedadId",
                table: "Propiedad");

            migrationBuilder.DropIndex(
                name: "IX_Propiedad_TipoVentaId",
                table: "Propiedad");

            migrationBuilder.DropColumn(
                name: "TipoPropiedadId",
                table: "Propiedad");

            migrationBuilder.DropColumn(
                name: "TipoVentaId",
                table: "Propiedad");

            migrationBuilder.AddColumn<string>(
                name: "TipoPropiedad",
                table: "Propiedad",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TipoVenta",
                table: "Propiedad",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
