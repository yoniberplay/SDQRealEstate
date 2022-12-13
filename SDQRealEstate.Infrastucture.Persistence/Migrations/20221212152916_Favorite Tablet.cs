using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDQRealEstate.Infrastucture.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FavoriteTablet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Favorita",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdPropiedad = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorita", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorita_Propiedad_IdPropiedad",
                        column: x => x.IdPropiedad,
                        principalTable: "Propiedad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorita_IdPropiedad",
                table: "Favorita",
                column: "IdPropiedad");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorita");
        }
    }
}
