using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Restructureunitandproductcollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductUnits_ProductUnitId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductUnits",
                schema: "lookup");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductUnitId",
                schema: "product",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductUnitId",
                schema: "product",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                schema: "product",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BaseUnits",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedById = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseUnits_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseUnits_Users_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitConversions",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BaseUnitId = table.Column<int>(type: "int", nullable: false),
                    ConversionValue = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedById = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitConversions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitConversions_BaseUnits_BaseUnitId",
                        column: x => x.BaseUnitId,
                        principalSchema: "lookup",
                        principalTable: "BaseUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnitConversions_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitConversions_Users_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseUnits_CreatedById",
                schema: "lookup",
                table: "BaseUnits",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BaseUnits_LastUpdatedById",
                schema: "lookup",
                table: "BaseUnits",
                column: "LastUpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitConversions_BaseUnitId",
                schema: "lookup",
                table: "UnitConversions",
                column: "BaseUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitConversions_CreatedById",
                schema: "lookup",
                table: "UnitConversions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitConversions_LastUpdatedById",
                schema: "lookup",
                table: "UnitConversions",
                column: "LastUpdatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnitConversions",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "BaseUnits",
                schema: "lookup");

            migrationBuilder.DropColumn(
                name: "UnitId",
                schema: "product",
                table: "Products");

            migrationBuilder.AddColumn<short>(
                name: "ProductUnitId",
                schema: "product",
                table: "Products",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.CreateTable(
                name: "ProductUnits",
                schema: "lookup",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    LastUpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductUnits_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductUnits_Users_LastUpdatedById",
                        column: x => x.LastUpdatedById,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductUnitId",
                schema: "product",
                table: "Products",
                column: "ProductUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_CreatedById",
                schema: "lookup",
                table: "ProductUnits",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductUnits_LastUpdatedById",
                schema: "lookup",
                table: "ProductUnits",
                column: "LastUpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductUnits_ProductUnitId",
                schema: "product",
                table: "Products",
                column: "ProductUnitId",
                principalSchema: "lookup",
                principalTable: "ProductUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
