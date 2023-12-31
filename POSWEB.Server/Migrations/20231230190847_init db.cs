using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSWEB.Server.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "product");

            migrationBuilder.EnsureSchema(
                name: "lookup");

            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.CreateTable(
                name: "ProductUnits",
                schema: "lookup",
                columns: table => new
                {
                    UnitId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductUnits", x => x.UnitId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "user",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ProductCategories",
                schema: "product",
                columns: table => new
                {
                    CategoryId = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    UserId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_ProductCategories_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_ProductCategories_Users_UserId1",
                        column: x => x.UserId1,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "product",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomBarcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductCategoryCategoryId = table.Column<short>(type: "smallint", nullable: false),
                    ProductUnitUnitId = table.Column<short>(type: "smallint", nullable: false),
                    IsRawMaterial = table.Column<bool>(type: "bit", nullable: false),
                    IsFinishedGoods = table.Column<bool>(type: "bit", nullable: false),
                    ReOrederLevel = table.Column<long>(type: "bigint", nullable: true),
                    PurchaseRate = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    SellingRate = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    WholesalePrice = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    VatPercent = table.Column<decimal>(type: "decimal(3,2)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductsCreated = table.Column<int>(type: "int", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductsUpdated = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_ProductCategories_ProductCategoryCategoryId",
                        column: x => x.ProductCategoryCategoryId,
                        principalSchema: "product",
                        principalTable: "ProductCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductUnits_ProductUnitUnitId",
                        column: x => x.ProductUnitUnitId,
                        principalSchema: "lookup",
                        principalTable: "ProductUnits",
                        principalColumn: "UnitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Users_ProductsCreated",
                        column: x => x.ProductsCreated,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Users_ProductsUpdated",
                        column: x => x.ProductsUpdated,
                        principalSchema: "user",
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_UserId",
                schema: "product",
                table: "ProductCategories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCategories_UserId1",
                schema: "product",
                table: "ProductCategories",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductCategoryCategoryId",
                schema: "product",
                table: "Products",
                column: "ProductCategoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductsCreated",
                schema: "product",
                table: "Products",
                column: "ProductsCreated");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductsUpdated",
                schema: "product",
                table: "Products",
                column: "ProductsUpdated");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductUnitUnitId",
                schema: "product",
                table: "Products",
                column: "ProductUnitUnitId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products",
                schema: "product");

            migrationBuilder.DropTable(
                name: "ProductCategories",
                schema: "product");

            migrationBuilder.DropTable(
                name: "ProductUnits",
                schema: "lookup");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "user");
        }
    }
}
